using NUnit.Framework;
using FalkonryClient.Helper.Models;
using System.Collections.Generic;

namespace FalkonryClient.Tests
{
  [TestFixture()]
  public class TestDataStream
  {
    static string host = System.Environment.GetEnvironmentVariable("FALKONRY_HOST_URL");
    static string token = System.Environment.GetEnvironmentVariable("FALKONRY_TOKEN");
    readonly Falkonry _falkonry = new Falkonry(host, token);
    List<Datastream> _datastreams = new List<Datastream>();

    // Create StandAlone Datastream with Wide format
    [Test()]
    public void CreateStandaloneDatastream()
    {
      var time = new Time
      {
        Zone = "Asia/Kolkata",
        Identifier = "time",
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
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    // Create Datastream for narrow/historian style data from a single entity
    [Test()]
    public void createDatastreamNarrowFormatSingleEntity()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      var ds = new DatastreamRequest();
      var Field = new Field();
      var Signal = new Signal();
      Signal.ValueIdentifier = "value";
      Signal.SignalIdentifier = "signal";
      Field.Signal = Signal;
      Field.Time = time;
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(ds.Field.Signal.ValueIdentifier, datastream.Field.Signal.ValueIdentifier);
        Assert.AreEqual(ds.Field.Signal.SignalIdentifier, datastream.Field.Signal.SignalIdentifier);
        Assert.AreEqual(datastream.Field.EntityName, datastream.Name);
      }
      catch (System.Exception exception)
      {

        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    // Create Datastream for older narrow/historian style data from a single entity
    [Test()]
    public void createDatastreamOlderNarrowFormatSingleEntity()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      var ds = new DatastreamRequest();
      var Field = new Field();
      var Signal = new Signal();
      Signal.ValueIdentifier = "value";
      Signal.TagIdentifier = "tag";
      Signal.IsSignalPrefix = true;
      Signal.Delimiter = "-";
      Field.Signal = Signal;
      Field.Time = time;
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(ds.Field.Signal.ValueIdentifier, datastream.Field.Signal.ValueIdentifier);
        Assert.AreEqual(ds.Field.Signal.TagIdentifier, datastream.Field.Signal.TagIdentifier);
        Assert.AreEqual(ds.Field.Signal.IsSignalPrefix, datastream.Field.Signal.IsSignalPrefix);
        Assert.AreEqual(ds.Field.Signal.Delimiter, datastream.Field.Signal.Delimiter);
      }
      catch (System.Exception exception)
      {

        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    // Create Datastream for narrow/historian style data from a Multi entity
    [Test()]
    public void createDatastreamNarrowFormatMultipleEntity()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      var ds = new DatastreamRequest();
      var Field = new Field();
      var Signal = new Signal();
      Signal.ValueIdentifier = "value";
      Signal.SignalIdentifier = "signal";
      Field.Signal = Signal;
      Field.Time = time;
      Field.EntityIdentifier = "entity";
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(ds.Field.Signal.ValueIdentifier, datastream.Field.Signal.ValueIdentifier);
        Assert.AreEqual(ds.Field.Signal.SignalIdentifier, datastream.Field.Signal.SignalIdentifier);
        Assert.AreEqual(datastream.Field.EntityIdentifier, "entity");
      }
      catch (System.Exception exception)
      {

        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    // Create Datastream for wide style data from a single entity
    [Test()]
    public void createDatastreamWideFormatSingleEntity()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      var ds = new DatastreamRequest();
      // Input List
      var inputList = new List<Input>();
      var currents = new Input();
      currents.Name = "current";
      currents.ValueType = new ValueType();
      currents.EventType = new EventType();
      currents.ValueType.Type = "Numeric";
      currents.EventType.Type = "Samples";
      inputList.Add(currents);

      var vibration = new Input();
      vibration.Name = "vibration";
      vibration.ValueType = new ValueType();
      vibration.EventType = new EventType();
      vibration.ValueType.Type = "Numeric";
      vibration.EventType.Type = "Samples";
      inputList.Add(vibration);

      var state = new Input();
      state.Name = "state";
      state.ValueType = new ValueType();
      state.EventType = new EventType();
      state.ValueType.Type = "Categorical";
      state.EventType.Type = "Samples";
      inputList.Add(state);

      ds.InputList = inputList;
      var Field = new Field();
      var Signal = new Signal();
      Field.Signal = Signal;
      Field.Time = time;
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(datastream.Field.EntityIdentifier, "entity");
        Assert.AreEqual(datastream.Field.EntityName, datastream.Name);
        Assert.AreEqual(datastream.InputList.Count, 3);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    // Create Datastream for wide style data from a multiple entities
    [Test()]
    public void createDatastreamWideFormatMultipleEntities()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      var ds = new DatastreamRequest();
      // Input List
      var inputList = new List<Input>();
      var currents = new Input();
      currents.Name = "current";
      currents.ValueType = new ValueType();
      currents.EventType = new EventType();
      currents.ValueType.Type = "Numeric";
      currents.EventType.Type = "Samples";
      inputList.Add(currents);

      var vibration = new Input();
      vibration.Name = "vibration";
      vibration.ValueType = new ValueType();
      vibration.EventType = new EventType();
      vibration.ValueType.Type = "Numeric";
      vibration.EventType.Type = "Samples";
      inputList.Add(vibration);

      var state = new Input();
      state.Name = "state";
      state.ValueType = new ValueType();
      state.EventType = new EventType();
      state.ValueType.Type = "Categorical";
      state.EventType.Type = "Samples";
      inputList.Add(state);

      ds.InputList = inputList;
      var Field = new Field();
      var Signal = new Signal();
      Field.Signal = Signal;
      Field.Time = time;
      Field.EntityIdentifier = "car";
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(ds.Field.EntityIdentifier, datastream.Field.EntityIdentifier);
        Assert.AreEqual(datastream.InputList.Count, 3);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }

    //Can not create datastream without name
    [Test()]
    public void CreateDatastreamWithoutName()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      //ds.Name = "TestDatastream" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        Assert.AreEqual(true, false, "No exception in case of missing datastream name");
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, "Missing name.", "Incorrect error message in case of missing name");
      }
    }

    //Can not create datastream without time identifier
    [Test()]
    public void CreateDatastreamWithoutTimeIdentifier()
    {
      var time = new Time();
      time.Zone = "GMT";
      //time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDatastream" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        Assert.AreEqual(true, false, "No exception in case of missing time identifier");
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, "Missing time identifier.", "Incorrect error message in case of missing time identifier");
      }


    }

    //Can not create datastream without time format
    [Test()]
    public void CreateDatastreamWithoutTimeFormat()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      //time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDatastream" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        Assert.AreEqual(true, false, "No exception in case of missing time format");
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, "Missing time format.", "Incorrect error message in case of missing time format");
      }
    }

    //Create Standalone datastream with entityIdentifier
    [Test()]
    public void CreateDatastreamWithEntityIdentifierTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      ds.Name = "TestDatastream" + randomNumber;
      var Field = new Field();

      Field.EntityIdentifier = "Unit";
      ds.Field = Field;
      ds.DataSource = datasource;
      ds.Field.Time = time;
      var datastream = _falkonry.CreateDatastream(ds);
      _datastreams.Add(datastream);
      Assert.AreEqual(ds.Name, datastream.Name);
      Assert.AreNotEqual(null, datastream.Id);
      Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
      Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
      Assert.AreEqual(ds.Field.EntityIdentifier, datastream.Field.EntityIdentifier);
      Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
    }

    //Create Standalone datastream with batchIdentifier
    [Test()]
    public void CreateDatastreamWithBatchIdentifierTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      ds.Name = "TestDatastream" + randomNumber;
      var Field = new Field();

      Field.BatchIdentifier = "Batch";
      ds.Field = Field;
      ds.DataSource = datasource;
      ds.Field.Time = time;
      var datastream = _falkonry.CreateDatastream(ds);
      _datastreams.Add(datastream);
      Assert.AreEqual(ds.Name, datastream.Name);
      Assert.AreNotEqual(null, datastream.Id);
      Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
      Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
      Assert.AreEqual(ds.Field.BatchIdentifier, datastream.Field.BatchIdentifier);
      Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
    }

    // Create PI Datastream (Narrow Format)
    [Test()]
    public void CreatePiDatastreamTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "PI";
      datasource.Host = "https://test.piserver.com/piwebapi";
      datasource.ElementTemplateName = "SampleElementTempalte";
      var ds = new DatastreamRequest();
      var Field = new Field();
      var Signal = new Signal();
      Signal.ValueIdentifier = "value";
      Signal.SignalIdentifier = "signal";
      Field.Signal = Signal;
      Field.Time = time;
      ds.Field = Field;
      ds.DataSource = datasource;
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      ds.Name = "TestDS" + randomNumber;
      ds.Field.Time = time;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }

    }

    // Retrieve Datastreams
    [Test()]
    public void GetDatastreamsTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "Time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDS" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);

        // get datastreams list
        List<Datastream> datastreamList = _falkonry.GetDatastreams();
        Assert.AreEqual(datastreamList.Count > 0, true);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message + exception.StackTrace, null, "Cannot retrieve list of datastreams");
      }
    }

    // Retrieve Datastreams by id
    [Test()]
    public void GetDatastreamByIdTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "Time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDS" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);

        // get datastream by id
        Datastream datastreamFetched = _falkonry.GetDatastream(datastream.Id);
        Assert.AreEqual(ds.Name, datastreamFetched.Name);
        Assert.AreNotEqual(null, datastreamFetched.Id);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error retrieving datastream by id");
      }
    }

    // Delete Datastream by id
    [Test()]
    public void DeleteDatastreamByIdTest()
    {
      var time = new Time();
      time.Zone = "GMT";
      time.Identifier = "Time";
      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDS" + randomNumber;
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error deleting datastream");
      }
    }

    // Create StandAlone Datastream with microsecond precision
    [Test()]
    public void CreateMicrosecondsDatastream()
    {
      var time = new Time();
      time.Zone = "Asia/Kolkata";
      time.Identifier = "time";

      time.Format = "iso_8601";

      var datasource = new Datasource();
      datasource.Type = "STANDALONE";
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var ds = new DatastreamRequest();
      ds.Name = "TestDS" + randomNumber;
      ds.TimePrecision = "micro";
      var Field = new Field();
      Field.Time = time;

      ds.Field = Field;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
        Assert.AreEqual(ds.TimePrecision, datastream.TimePrecision);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error creating datastream");
      }
    }
    [TearDown()]
    public void Cleanup()
    {
      if(_datastreams.Count > 0)
      {
        var ds = _datastreams[0];
        _datastreams.Clear();
        _falkonry.DeleteDatastream(ds.Id);
      }
    }
  }
}
