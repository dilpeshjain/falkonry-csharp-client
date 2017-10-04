using System.Collections.Generic;
using falkonry_csharp_client.helper.models;
using falkonry_csharp_client.service;
using System;

namespace falkonry_csharp_client    
{
    public class Falkonry
    {
        private readonly FalkonryService _falkonryService;
        public Falkonry(string host, string token, SortedDictionary<string, string> _piOptions = null)
        {
            _falkonryService = new FalkonryService(host, token, _piOptions);
        }

        public Datastream CreateDatastream(DatastreamRequest datastream)
        {
            try
            {
                return _falkonryService.CreateDatastream(datastream);
            }catch (Exception)
            {
                throw;
            }
       
        }

        public List<Datastream> GetDatastreams()
        {
            try
            {
                return _falkonryService.GetDatastream();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Datastream GetDatastream(string datastream)
        {
            try
            {
                return _falkonryService.GetDatastream(datastream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteDatastream(string datastream)
        {
            try
            {
                _falkonryService.DeleteDatastream(datastream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Assessment CreateAssessment(AssessmentRequest assessment)
        {
            try
            {
                return _falkonryService.CreateAssessment(assessment);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Assessment> GetAssessments()
        {
            try
            {
                return _falkonryService.GetAssessment();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Assessment GetAssessment(string assessment)
        {
            try
            {
                return _falkonryService.GetAssessment(assessment);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteAssessment(string assessment)
        {
            try
            {
                _falkonryService.DeleteAssessment(assessment);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InputStatus AddInput(string datastream, string data, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.AddInputData(datastream, data, options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InputStatus AddInputStream(string datastream, byte[] stream, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.AddInputFromStream(datastream, stream, options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EventSource GetOutput(string assessment, long? start, long? end)
        {
            try
            {
                return _falkonryService.GetOutput(assessment, start, end);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddFacts(string assessment, string data, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.AddFacts(assessment, data, options);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string AddFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.AddFactsStream(assessment, stream, options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public HttpResponse GetHistoricalOutput(Assessment assessment, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.GetHistoricalOutput(assessment, options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EntityMeta> PostEntityMeta(List<EntityMetaRequest> entityMetaRequest, Datastream datastream)
        {
            try
            {
                return _falkonryService.PostEntityMeta(entityMetaRequest, datastream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EntityMeta> GetEntityMeta(Datastream datastream)
        {
            try
            {
                return _falkonryService.GetEntityMeta(datastream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void onDatastream(string datastreamId)
        {
            try
            {
                _falkonryService.onDatastream(datastreamId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void offDatastream(string datastreamId)
        {
            try
            {
                _falkonryService.offDatastream(datastreamId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponse getFacts(string assessment, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.GetFacts(assessment, options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public HttpResponse GetDatastreamData(string datastream, SortedDictionary<string, string> options)
        {
            try
            {
                return _falkonryService.GetDatastreamData(datastream, options);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }

}
