using NUnit.Framework;
using FalkonryClient.Helper.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace FalkonryClient.Tests
{
  [TestFixture()]
  public class TestAssessmentLive
  {
    static string host = System.Environment.GetEnvironmentVariable("FALKONRY_HOST_URL");
    static string token = System.Environment.GetEnvironmentVariable("FALKONRY_TOKEN");
    static string assessmentId = System.Environment.GetEnvironmentVariable("FALKONRY_ASSESSMENT_SLIDING_ID");

    readonly Falkonry _falkonry = new Falkonry(host, token);
    List<Datastream> _datastreams = new List<Datastream>();

    // Should get live monitoring status of assessment
    [Test()]
    public void testLiveMonitoringStatus()
    {
      Assessment assessment = _falkonry.GetAssessment(assessmentId);
      string[] liveStatus = { "ON", "OFF" };
      Assert.AreEqual(true, System.Array.IndexOf(liveStatus, assessment.Live) >= 0);
    }


    // Should get exception when turning on the assessment without active model
    [Test()]
    public void testOnAssessmentException()
    {
      var time = new Time
      {
        Zone = "GMT",
        Identifier = "Time",
        Format = "iso_8601"
      };
      var datasource = new Datasource
      {
        Type = "STANDALONE"
      };
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));

      var Field = new Field
      {
        Time = time
      };
      var ds = new DatastreamRequest
      {
        Name = "TestDS" + randomNumber,
        Field = Field,
        DataSource = datasource
      };
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotSame(null, datastream.Id);
        // create assessment
        AssessmentRequest asmtRequest = new AssessmentRequest
        {
          Name = "TestAsmt",
          Datastream = datastream.Id
        };
        var assessmentCreated = _falkonry.CreateAssessment(asmtRequest);

        try
        {
          var liveAssessment = _falkonry.onAssessment(assessmentCreated.Id);
          Assert.AreEqual(liveAssessment.Id, assessmentCreated.Id);
        }
        catch (System.Exception exception)
        {
          Assert.AreEqual(exception.Message, "No Active model assigned in Assessment: " + assessmentCreated.Name);
        }

      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "CRUD operation failed for assessment");
      }
    }


    // Should turn on and off the assessment
    [Test()]
    public void testOnOffAssessment()
    {
      // Here assessment should have an active model
      Assessment assessment1 = _falkonry.onAssessment(assessmentId);
      Assert.AreEqual(assessment1.Id, assessmentId);

      System.Threading.Thread.Sleep(30000);

      Assessment assessment2 = _falkonry.offAssessment(assessmentId);
      Assert.AreEqual(assessment2.Id, assessmentId);

      System.Threading.Thread.Sleep(30000);
    }

    [TearDown()]
    public void Cleanup()
    {
      if (_datastreams.Count > 0)
      {
        var ds = _datastreams[0];
        _datastreams.Clear();
        _falkonry.DeleteDatastream(ds.Id);
      }
    }

  }
}