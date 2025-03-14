using AccessManagementLaredo.HelperModels;
using DataTier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccessManagementLaredo
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IPermitRequestResidentialRepository : Interfaces.IRepository<PermitRequestResidentialHelperModel>
    {
        public void CreateEvent(int sequenceValue, string eventTypeCode, string userName, string userRoleName, string comment);
        public string ReadPermitRequests(int? id = -1);
        public void UpdateInternalSection(PermitRequestInternalReviewHelperModel entity, int id);
        public void UpdateStatus(PermitRequestStatusHelperModel entity);
        public IEnumerable<AttachmentHelperModel> ReadAttachments(int? id = -1);

	}

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class PermitRequestResidential
    {

    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class PermitRequestResidentialRepository : IPermitRequestResidentialRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();
        private PermitEventHelperModel _permitEventHelperModel;
        private Tools _tools;
        //private PermitEventRepository _permitEventRepository;

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public PermitRequestResidentialRepository(Interfaces.IUnitOfWork unitOfWork, PermitEventHelperModel permitEventHelperModel, Tools tools)
        {
            _unitOfWork = unitOfWork;
            _permitEventHelperModel = permitEventHelperModel;
            _tools = tools;
            //_permitEventRepository = permitEventRepository;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(PermitRequestResidentialHelperModel entity)
        {
            //ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO PRMT_RQST (");
            _strQuery.Append("HWY_ID, ");
            _strQuery.Append("PRMT_RQST_SUBMIT_DT, ");
            _strQuery.Append("CNSTR_TYPE_ID, ");
            _strQuery.Append("HWY_PRFX_ID, ");
            _strQuery.Append("PRMT_RQST_TYPE, ");
            _strQuery.Append("CNTY_ID, ");
            _strQuery.Append("DIST_ID, ");
            _strQuery.Append("PRMT_RQST_FRST_NM, ");
            _strQuery.Append("PRMT_RQST_LAST_NM, ");
            _strQuery.Append("PRMT_RQST_ADDR, ");
            _strQuery.Append("PRMT_RQST_CITY_NM, ");
            _strQuery.Append("PRMT_RQST_ZIPCD, ");
            _strQuery.Append("PRMT_RQST_STATE_CD, ");
            _strQuery.Append("PRMT_RQST_PHN_NBR, ");
            _strQuery.Append("PRMT_RQST_LON, ");
            _strQuery.Append("PRMT_RQST_LAT, ");
            _strQuery.Append("PRMT_RQST_CNSTR_STRT_DT, ");
			_strQuery.Append("PRMT_RQST_LITIG_FLAG, ");
			_strQuery.Append("PRMT_RQST_CNSTR_FLAG, ");
            _strQuery.Append("PRMT_RQST_STATE_REP_NM, ");
            _strQuery.Append("PRMT_RQST_STATE_REP_PHN_NBR, ");
            //_strQuery.Append("PRMT_RQST_OTHR_COND_TXT, ");
            //_strQuery.Append("PRMT_RQST_VARNC_ONE_FLAG, ");
            //_strQuery.Append("PRMT_RQST_VARNC_TWO_FLAG, ");
            //_strQuery.Append("PRMT_RQST_VARNC_JUST_TXT, ");
            //_strQuery.Append("PRMT_RQST_VARNC_DENL_ONE_FLAG, ");
            //_strQuery.Append("PRMT_RQST_VARNC_DENL_TWO_FLAG, ");
            _strQuery.Append("PRMT_RQST_STAT_TRAFF_CD, ");
            _strQuery.Append("PRMT_RQST_STAT_TPD_CD, ");
            _strQuery.Append("PRMT_RQST_STAT_AREA_OFFC_CD, ");
			_strQuery.Append("PRMT_RQST_STAT_EXT_CD");
			//_strQuery.Append("PRMT_RQST_REQ_TRAFF, ");
   //         _strQuery.Append("PRMT_RQST_REQ_TPD");
            _strQuery.Append(") OUTPUT inserted.PRMT_RQST_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_highway, ");
            _strQuery.Append("@prm_prmt_rqst_submit_date, ");
            _strQuery.Append("@prm_construction_type, ");
            _strQuery.Append("@prm_highway_prefix, ");
            _strQuery.Append("@prm_prmt_rqst_type, ");
            _strQuery.Append("@prm_county, ");
            _strQuery.Append("@prm_district, ");
            _strQuery.Append("@prm_prmt_rqst_first_name, ");
            _strQuery.Append("@prm_prmt_rqst_last_name, ");
            _strQuery.Append("@prm_prmt_rqst_address, ");
            _strQuery.Append("@prm_prmt_rqst_city, ");
            _strQuery.Append("@prm_prmt_rqst_zipcode, ");
            _strQuery.Append("@prm_prmt_rqst_state, ");
            _strQuery.Append("@prm_prmt_rqst_phone, ");
            _strQuery.Append("@prm_prmt_rqst_longitude, ");
            _strQuery.Append("@prm_prmt_rqst_latitude, ");
			_strQuery.Append("@prm_prmt_construction_start_date, ");
			_strQuery.Append("@prm_prmt_rqst_litigation_flag, ");
            _strQuery.Append("@prm_prmt_rqst_construction_flag, ");
            _strQuery.Append("@prm_prmt_rqst_state_rep_name, ");
            _strQuery.Append("@prm_prmt_rqst_state_rep_number, ");
            //_strQuery.Append("@prm_prmt_rqst_other_conditions, ");
            //_strQuery.Append("@prm_prmt_rqst_variance_one_flag, ");
            //_strQuery.Append("@prm_prmt_rqst_variance_two_flag, ");
            //_strQuery.Append("@prm_prmt_rqst_variance_justification, ");
            //_strQuery.Append("@prm_prmt_rqst_variance_denial_one_flag, ");
            //_strQuery.Append("@prm_prmt_rqst_variance_denial_two_flag, ");
            _strQuery.Append("@prm_prmt_rqst_status_traffic, ");
            _strQuery.Append("@prm_prmt_rqst_status_tdp, ");
            _strQuery.Append("@prm_prmt_rqst_status_area_office, ");
			_strQuery.Append("@prm_prmt_rqst_status_external");
			//_strQuery.Append("@prm_prmt_rqst_requires_traffic, ");
   //         _strQuery.Append("@prm_prmt_rqst_requires_tpd");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_highway", entity.HighwayId);
            _queryParams.Add("prm_prmt_rqst_submit_date", entity.SubmitDate);
            _queryParams.Add("prm_construction_type", entity.ConstructionTypeId);
            _queryParams.Add("prm_highway_prefix", entity.HighwayPrefixId);
            _queryParams.Add("prm_prmt_rqst_type", entity.RequestType);
            _queryParams.Add("prm_county", entity.CountyId);
            _queryParams.Add("prm_district", entity.DistrictId);
            _queryParams.Add("prm_prmt_rqst_first_name", entity.RequestorFirstName);
            _queryParams.Add("prm_prmt_rqst_last_name", entity.RequestorLastName);
            _queryParams.Add("prm_prmt_rqst_address", entity.RequestorAddress);
            _queryParams.Add("prm_prmt_rqst_city", entity.RequestorCity);
            _queryParams.Add("prm_prmt_rqst_zipcode", entity.RequestorZipCode);
            _queryParams.Add("prm_prmt_rqst_state", entity.RequestorState);
            _queryParams.Add("prm_prmt_rqst_phone", entity.RequestorPhoneNumber);
            _queryParams.Add("prm_prmt_rqst_longitude", entity.Longitude);
            _queryParams.Add("prm_prmt_rqst_latitude", entity.Latitude);
			_queryParams.Add("prm_prmt_construction_start_date", entity.ConstructionStartDate);
			_queryParams.Add("prm_prmt_rqst_litigation_flag", entity.LitigationFlag);
            _queryParams.Add("prm_prmt_rqst_construction_flag", entity.ConstructionFlag);
            _queryParams.Add("prm_prmt_rqst_state_rep_name", entity.StateRepresentativeName);
            _queryParams.Add("prm_prmt_rqst_state_rep_number", entity.StateRepresentativePhoneNumber);
            //_queryParams.Add("prm_prmt_rqst_other_conditions", entity.OtherConditions is null ? DBNull.Value : entity.OtherConditions);
            //_queryParams.Add("prm_prmt_rqst_variance_one_flag", entity.VarianceOneFlag);
            //_queryParams.Add("prm_prmt_rqst_variance_two_flag", entity.VarianceTwoFlag);
            //_queryParams.Add("prm_prmt_rqst_variance_justification", entity.VarianceJustification is null ? DBNull.Value : entity.VarianceJustification);
            //_queryParams.Add("prm_prmt_rqst_variance_denial_one_flag", entity.VarianceDenialOneFlag);
            //_queryParams.Add("prm_prmt_rqst_variance_denial_two_flag", entity.VarianceDenialTwoFlag);
            _queryParams.Add("prm_prmt_rqst_status_traffic", DBNull.Value);
            _queryParams.Add("prm_prmt_rqst_status_tdp", DBNull.Value);
            _queryParams.Add("prm_prmt_rqst_status_area_office", DBNull.Value);
			_queryParams.Add("prm_prmt_rqst_status_external", "DRFT");
			//_queryParams.Add("prm_prmt_rqst_requires_traffic", entity.RequiresTraffic);
   //         _queryParams.Add("prm_prmt_rqst_requires_tpd", entity.RequiresTPD);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            CreateResidential(entity, sequenceValue);

            return sequenceValue;
        }

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new attachment for residential form.
		// ---------------------------------------------------------------------------------------------
		private void CreateAttachments(PermitRequestResidentialHelperModel entity, int permitRequestId)
		{
			int sequenceValue;

			foreach (var item in entity.Attachments)
			{
				_strQuery.Clear();
				_strQuery.Append("INSERT INTO ATCHMT (");
				_strQuery.Append("ATCHMT_NM, ");
				_strQuery.Append("ATCHMT_TYPE_ID, ");
				_strQuery.Append("PRMT_RQST_ID, ");
				_strQuery.Append("ATCHMT_CMT");
				_strQuery.Append(") OUTPUT inserted.ATCHMT_ID ");
				_strQuery.Append("VALUES (");
				_strQuery.Append("@prm_atchmt_name, ");
				_strQuery.Append("@prm_atchmt_type_id, ");
				_strQuery.Append("@prm_prmt_rqst_id, ");
				_strQuery.Append("@prm_atchmt_cmt");
				_strQuery.Append(")");

				_queryParams.Clear();
				_queryParams.Add("prm_atchmt_name", item.Name);
				_queryParams.Add("prm_atchmt_type_id", item.AttachmentTypeId);
				_queryParams.Add("prm_prmt_rqst_id", permitRequestId);
				_queryParams.Add("prm_atchmt_cmt", item.Comment is null ? DBNull.Value : item.Comment);
				
				sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);
			}
		}

		// ---------------------------------------------------------------------------------------------
		//                  Insert a new record for residential form.
		// ---------------------------------------------------------------------------------------------
		public void CreateResidential(PermitRequestResidentialHelperModel entity, int sequenceValue)
        {
            //ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO RES_SKCH (");
            _strQuery.Append("RES_SKCH_PIPE_LNGTH_MS, ");
            _strQuery.Append("RES_SKCH_DWAY_WIDTH_MS, ");
            _strQuery.Append("RES_SKCH_DSTNCE_TO_CNTR_MS, ");
            _strQuery.Append("RES_SKCH_DSTNCE_FROM_EDGE_MS, ");
            _strQuery.Append("RES_SKCH_RADIUS_ONE_MS, ");
            _strQuery.Append("RES_SKCH_RADIUS_TWO_MS, ");
            _strQuery.Append("RES_SKCH_DITCH_DEPTH_MS, ");
            _strQuery.Append("RES_SKCH_PIPE_SZ_MS, ");
            _strQuery.Append("RES_SKCH_SET_BACK_WIDTH_GATE_MS, ");
            _strQuery.Append("RES_SKCH_SET_BACK_WIDTH_ROW_MS, ");
            _strQuery.Append("RES_SKCH_THROAT_LNGTH_MS, ");
            _strQuery.Append("PRMT_RQST_ID");
            _strQuery.Append(") OUTPUT inserted.RES_SKCH_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_res_skch_pipe_length, ");
            _strQuery.Append("@prm_res_skch_driveway_width, ");
            _strQuery.Append("@prm_res_skch_distance_to_center, ");
            _strQuery.Append("@prm_res_skch_distance_from_edge, ");
            _strQuery.Append("@prm_res_skch_radius_one, ");
            _strQuery.Append("@prm_res_skch_radius_two, ");
            _strQuery.Append("@prm_res_skch_drainage_pipe, ");
            _strQuery.Append("@prm_res_skch_drainage_structure, ");
            _strQuery.Append("@prm_res_skch_width_gate, ");
            _strQuery.Append("@prm_res_skch_width_row, ");
            _strQuery.Append("@prm_res_skch_throat_length, ");
            _strQuery.Append("@prm_prmt_rqst_id");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_res_skch_pipe_length", entity.PipeLength);
            _queryParams.Add("prm_res_skch_driveway_width", entity.DrivewayWidth);
            _queryParams.Add("prm_res_skch_distance_to_center", entity.DistanceToCenter);
            _queryParams.Add("prm_res_skch_distance_from_edge", entity.DistanceFromEdge);
            _queryParams.Add("prm_res_skch_radius_one", entity.RadiusOne);
            _queryParams.Add("prm_res_skch_radius_two", entity.RadiusTwo);
            _queryParams.Add("prm_res_skch_drainage_pipe", entity.DrainagePipe);
            _queryParams.Add("prm_res_skch_drainage_structure", entity.DrainageStructure);
            _queryParams.Add("prm_res_skch_width_gate", entity.WidthGate);
            _queryParams.Add("prm_res_skch_width_row", entity.WidthROW);
            _queryParams.Add("prm_res_skch_throat_length", entity.ThroatLength);
            _queryParams.Add("prm_prmt_rqst_id", sequenceValue);

            _unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record for an event. (is it still being used????)
        // ---------------------------------------------------------------------------------------------
        public void CreateEvent(int sequenceValue, string eventTypeCode, string userName, string userRoleName, string comment)
        {
            _permitEventHelperModel.DateCreated = DateTime.Now;
            _permitEventHelperModel.PermitRequestId = sequenceValue;
            _permitEventHelperModel.PermitEventTypeCode = eventTypeCode;
            _permitEventHelperModel.UserName = userName;
            _permitEventHelperModel.UserRoleName = userRoleName;
            _permitEventHelperModel.PermitEventComment = comment;

            PermitEventRepository permitEventRepository = new PermitEventRepository(_unitOfWork);
            permitEventRepository.Create(_permitEventHelperModel);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM DIST WHERE ");
            _strQuery.Append("DIST_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

		// ---------------------------------------------------------------------------------------------
		//                  Delete attachment record.
		// ---------------------------------------------------------------------------------------------
		private void DeleteAttachments(int id)
		{
			_strQuery.Clear();
			_strQuery.Append("DELETE FROM ATCHMT WHERE ");
			_strQuery.Append("PRMT_RQST_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Update record.
		// ---------------------------------------------------------------------------------------------
		public void Update(PermitRequestResidentialHelperModel entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE PRMT_RQST SET ");
			_strQuery.Append("HWY_ID = @prm_highway, ");
			_strQuery.Append("PRMT_RQST_SUBMIT_DT = @prm_prmt_rqst_submit_date, ");
			_strQuery.Append("CNSTR_TYPE_ID = @prm_construction_type, ");
			_strQuery.Append("HWY_PRFX_ID = @prm_highway_prefix, ");
			_strQuery.Append("PRMT_RQST_TYPE = @prm_prmt_rqst_type, ");
			_strQuery.Append("CNTY_ID = @prm_county, ");
			_strQuery.Append("DIST_ID = @prm_district, ");
			_strQuery.Append("PRMT_RQST_FRST_NM = @prm_prmt_rqst_first_name, ");
			_strQuery.Append("PRMT_RQST_LAST_NM = @prm_prmt_rqst_last_name, ");
			_strQuery.Append("PRMT_RQST_ADDR = @prm_prmt_rqst_address, ");
			_strQuery.Append("PRMT_RQST_CITY_NM = @prm_prmt_rqst_city, ");
			_strQuery.Append("PRMT_RQST_ZIPCD = @prm_prmt_rqst_zipcode, ");
			_strQuery.Append("PRMT_RQST_STATE_CD = @prm_prmt_rqst_state, ");
			_strQuery.Append("PRMT_RQST_PHN_NBR = @prm_prmt_rqst_phone, ");
			_strQuery.Append("PRMT_RQST_LON = @prm_prmt_rqst_longitude, ");
			_strQuery.Append("PRMT_RQST_LAT = @prm_prmt_rqst_latitude, ");
			_strQuery.Append("PRMT_RQST_CNSTR_STRT_DT = @prm_prmt_construction_start_date, ");
			_strQuery.Append("PRMT_RQST_LITIG_FLAG = @prm_prmt_rqst_litigation_flag, ");
			_strQuery.Append("PRMT_RQST_CNSTR_FLAG = @prm_prmt_rqst_construction_flag, ");
			_strQuery.Append("PRMT_RQST_STATE_REP_NM = @prm_prmt_rqst_state_rep_name, ");
			_strQuery.Append("PRMT_RQST_STATE_REP_PHN_NBR = @prm_prmt_rqst_state_rep_number ");
			//_strQuery.Append("PRMT_RQST_OTHR_COND_TXT = @prm_prmt_rqst_other_conditions, ");
			//_strQuery.Append("PRMT_RQST_VARNC_ONE_FLAG = @prm_prmt_rqst_variance_one_flag, ");
			//_strQuery.Append("PRMT_RQST_VARNC_TWO_FLAG = @prm_prmt_rqst_variance_two_flag, ");
			//_strQuery.Append("PRMT_RQST_VARNC_JUST_TXT = @prm_prmt_rqst_variance_justification, ");
			//_strQuery.Append("PRMT_RQST_VARNC_DENL_ONE_FLAG = @prm_prmt_rqst_variance_denial_one_flag, ");
			//_strQuery.Append("PRMT_RQST_VARNC_DENL_TWO_FLAG = @prm_prmt_rqst_variance_denial_two_flag, ");
   //         _strQuery.Append("PRMT_RQST_REQ_TRAFF = @prm_prmt_rqst_requires_traffic, ");
   //         _strQuery.Append("PRMT_RQST_REQ_TPD = @prm_prmt_rqst_requires_tpd ");
            //_strQuery.Append("PRMT_RQST_STAT_TRAFF = @prm_prmt_rqst_status_traffic, ");
            //_strQuery.Append("PRMT_RQST_STAT_TPD = @prm_prmt_rqst_status_tpd, ");
            //_strQuery.Append("PRMT_RQST_STAT_AREA_OFFC = @prm_prmt_rqst_status_area_office ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("PRMT_RQST_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
			_queryParams.Add("prm_highway", entity.HighwayId);
			_queryParams.Add("prm_prmt_rqst_submit_date", entity.SubmitDate);
			_queryParams.Add("prm_construction_type", entity.ConstructionTypeId);
			_queryParams.Add("prm_highway_prefix", entity.HighwayPrefixId);
			_queryParams.Add("prm_prmt_rqst_type", entity.RequestType);
			_queryParams.Add("prm_county", entity.CountyId);
			_queryParams.Add("prm_district", entity.DistrictId);
			_queryParams.Add("prm_prmt_rqst_first_name", entity.RequestorFirstName);
			_queryParams.Add("prm_prmt_rqst_last_name", entity.RequestorLastName);
			_queryParams.Add("prm_prmt_rqst_address", entity.RequestorAddress);
			_queryParams.Add("prm_prmt_rqst_city", entity.RequestorCity);
			_queryParams.Add("prm_prmt_rqst_zipcode", entity.RequestorZipCode);
			_queryParams.Add("prm_prmt_rqst_state", entity.RequestorState);
			_queryParams.Add("prm_prmt_rqst_phone", entity.RequestorPhoneNumber);
			_queryParams.Add("prm_prmt_rqst_longitude", entity.Longitude);
			_queryParams.Add("prm_prmt_rqst_latitude", entity.Latitude);
			_queryParams.Add("prm_prmt_construction_start_date", entity.ConstructionStartDate);
			_queryParams.Add("prm_prmt_rqst_litigation_flag", entity.LitigationFlag);
			_queryParams.Add("prm_prmt_rqst_construction_flag", entity.ConstructionFlag);
			_queryParams.Add("prm_prmt_rqst_state_rep_name", entity.StateRepresentativeName);
			_queryParams.Add("prm_prmt_rqst_state_rep_number", entity.StateRepresentativePhoneNumber);
			//_queryParams.Add("prm_prmt_rqst_other_conditions", entity.OtherConditions is null ? DBNull.Value : entity.OtherConditions);
			//_queryParams.Add("prm_prmt_rqst_variance_one_flag", entity.VarianceOneFlag);
			//_queryParams.Add("prm_prmt_rqst_variance_two_flag", entity.VarianceTwoFlag);
			//_queryParams.Add("prm_prmt_rqst_variance_justification", entity.VarianceJustification is null ? DBNull.Value : entity.VarianceJustification);
			//_queryParams.Add("prm_prmt_rqst_variance_denial_one_flag", entity.VarianceDenialOneFlag);
			//_queryParams.Add("prm_prmt_rqst_variance_denial_two_flag", entity.VarianceDenialTwoFlag);
   //         _queryParams.Add("prm_prmt_rqst_requires_traffic", entity.RequiresTraffic);
   //         _queryParams.Add("prm_prmt_rqst_requires_tpd", entity.RequiresTPD);
            //_queryParams.Add("prm_prmt_rqst_status_traffic", entity.StatusTraffic);
            //_queryParams.Add("prm_prmt_rqst_status_tpd", entity.StatusTPD);
            //_queryParams.Add("prm_prmt_rqst_status_area_office", entity.StatusAreaOffice);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
            UpdateResidential(entity, id);
            DeleteAttachments(id);
            CreateAttachments(entity, id);
        }

		// ---------------------------------------------------------------------------------------------
		//                  Update residential record.
		// ---------------------------------------------------------------------------------------------
		public void UpdateResidential(PermitRequestResidentialHelperModel entity, int id)
		{
			ConvertCase(entity);

			_strQuery.Clear();
			_strQuery.Append("UPDATE RES_SKCH SET ");
			_strQuery.Append("RES_SKCH_PIPE_LNGTH_MS = @prm_res_skch_pipe_length, ");
			_strQuery.Append("RES_SKCH_DWAY_WIDTH_MS = @prm_res_skch_driveway_width, ");
			_strQuery.Append("RES_SKCH_DSTNCE_TO_CNTR_MS = @prm_res_skch_distance_to_center, ");
			_strQuery.Append("RES_SKCH_DSTNCE_FROM_EDGE_MS = @prm_res_skch_distance_from_edge, ");
			_strQuery.Append("RES_SKCH_RADIUS_ONE_MS = @prm_res_skch_radius_one, ");
			_strQuery.Append("RES_SKCH_RADIUS_TWO_MS = @prm_res_skch_radius_two, ");
			_strQuery.Append("RES_SKCH_DITCH_DEPTH_MS = @prm_res_skch_drainage_pipe, ");
			_strQuery.Append("RES_SKCH_PIPE_SZ_MS = @prm_res_skch_drainage_structure, ");
			_strQuery.Append("RES_SKCH_SET_BACK_WIDTH_GATE_MS = @prm_res_skch_width_gate, ");
			_strQuery.Append("RES_SKCH_SET_BACK_WIDTH_ROW_MS = @prm_res_skch_width_row, ");
			_strQuery.Append("RES_SKCH_THROAT_LNGTH_MS =  @prm_res_skch_throat_length ");
			//_strQuery.Append("PRMT_RQST_ID");
			_strQuery.Append("WHERE ");
			_strQuery.Append("PRMT_RQST_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", id);
			_queryParams.Add("prm_res_skch_pipe_length", entity.PipeLength);
			_queryParams.Add("prm_res_skch_driveway_width", entity.DrivewayWidth);
			_queryParams.Add("prm_res_skch_distance_to_center", entity.DistanceToCenter);
			_queryParams.Add("prm_res_skch_distance_from_edge", entity.DistanceFromEdge);
			_queryParams.Add("prm_res_skch_radius_one", entity.RadiusOne);
			_queryParams.Add("prm_res_skch_radius_two", entity.RadiusTwo);
			_queryParams.Add("prm_res_skch_drainage_pipe", entity.DrainagePipe);
			_queryParams.Add("prm_res_skch_drainage_structure", entity.DrainageStructure);
			_queryParams.Add("prm_res_skch_width_gate", entity.WidthGate);
			_queryParams.Add("prm_res_skch_width_row", entity.WidthROW);
			_queryParams.Add("prm_res_skch_throat_length", entity.ThroatLength);

			_unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

        // ---------------------------------------------------------------------------------------------
        //                  Update internal review section of request form.
        // ---------------------------------------------------------------------------------------------
        public void UpdateInternalSection(PermitRequestInternalReviewHelperModel entity, int id)
        {
            //ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE PRMT_RQST SET ");
            _strQuery.Append("PRMT_RQST_OTHR_COND_TXT = @prm_prmt_rqst_other_conditions, ");
            _strQuery.Append("PRMT_RQST_VARNC_ONE_FLAG = @prm_prmt_rqst_variance_one_flag, ");
            _strQuery.Append("PRMT_RQST_VARNC_TWO_FLAG = @prm_prmt_rqst_variance_two_flag, ");
            _strQuery.Append("PRMT_RQST_VARNC_JUST_TXT = @prm_prmt_rqst_variance_justification, ");
            _strQuery.Append("PRMT_RQST_VARNC_DENL_ONE_FLAG = @prm_prmt_rqst_variance_denial_one_flag, ");
            _strQuery.Append("PRMT_RQST_VARNC_DENL_TWO_FLAG = @prm_prmt_rqst_variance_denial_two_flag, ");
            _strQuery.Append("PRMT_RQST_REQ_TRAFF = @prm_prmt_rqst_requires_traffic, ");
            _strQuery.Append("PRMT_RQST_REQ_TPD = @prm_prmt_rqst_requires_tpd ");

            _strQuery.Append("WHERE ");
            _strQuery.Append("PRMT_RQST_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_prmt_rqst_other_conditions", entity.OtherConditions is null ? DBNull.Value : entity.OtherConditions.ToUpper());
            _queryParams.Add("prm_prmt_rqst_variance_one_flag", entity.VarianceOneFlag);
            _queryParams.Add("prm_prmt_rqst_variance_two_flag", entity.VarianceTwoFlag);
            _queryParams.Add("prm_prmt_rqst_variance_justification", entity.VarianceJustification is null ? DBNull.Value : entity.VarianceJustification.ToUpper());
            _queryParams.Add("prm_prmt_rqst_variance_denial_one_flag", entity.VarianceDenialOneFlag);
            _queryParams.Add("prm_prmt_rqst_variance_denial_two_flag", entity.VarianceDenialTwoFlag);
            _queryParams.Add("prm_prmt_rqst_requires_traffic", entity.RequiresTraffic);
            _queryParams.Add("prm_prmt_rqst_requires_tpd", entity.RequiresTPD);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                      Update status of a permit request.
        // ---------------------------------------------------------------------------------------------
        public void UpdateStatus(PermitRequestStatusHelperModel entity)
        {
			_strQuery.Clear();
			_strQuery.Append("UPDATE PRMT_RQST SET ");
			_strQuery.Append("PRMT_RQST_STAT_TRAFF_CD = @prm_prmt_rqst_status_traffic, ");
			_strQuery.Append("PRMT_RQST_STAT_TPD_CD = @prm_prmt_rqst_status_tpd, ");
			_strQuery.Append("PRMT_RQST_STAT_AREA_OFFC_CD = @prm_prmt_rqst_status_area_office, ");
            _strQuery.Append("PRMT_RQST_STAT_EXT_CD = @prm_prmt_rqst_status_external ");


            //_strQuery.Append("PRMT_RQST_STAT_TRAFF = ");
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN PRMT_RQST_STAT_TRAFF <> 'AA' THEN @prm_prmt_rqst_status_traffic ");
            //_strQuery.Append("ELSE @prm_prmt_rqst_status_approved ");
            //_strQuery.Append("END, ");

            //_strQuery.Append("PRMT_RQST_STAT_TPD = ");
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN PRMT_RQST_STAT_TRAFF <> 'AA' AND PRMT_RQST_STAT_TRAFF <> 'DD' THEN @prm_prmt_rqst_status_pending ");
            //_strQuery.Append("WHEN PRMT_RQST_STAT_TPD <> 'AA' THEN @prm_prmt_rqst_status_tpd ");
            //_strQuery.Append("ELSE @prm_prmt_rqst_status_approved ");
            //_strQuery.Append("END, ");

            //_strQuery.Append("PRMT_RQST_STAT_AREA_OFFC = ");
            //_strQuery.Append("CASE ");
            //_strQuery.Append("WHEN PRMT_RQST_STAT_TPD <> 'AA' AND PRMT_RQST_STAT_TPD <> 'DD' THEN @prm_prmt_rqst_status_pending ");
            //_strQuery.Append("WHEN PRMT_RQST_STAT_AREA_OFFC <> 'AA' THEN @prm_prmt_rqst_status_area_office ");
            //_strQuery.Append("ELSE @prm_prmt_rqst_status_approved ");
            //_strQuery.Append("END ");

            _strQuery.Append("WHERE ");
			_strQuery.Append("PRMT_RQST_ID = @prm_id");

			_queryParams.Clear();
			_queryParams.Add("prm_id", entity.Id);
            _queryParams.Add("prm_prmt_rqst_status_traffic", entity.StatusTrafficCode is null ? DBNull.Value : entity.StatusTrafficCode);
            _queryParams.Add("prm_prmt_rqst_status_tpd", entity.StatusTPDCode is null ? DBNull.Value : entity.StatusTPDCode);
            _queryParams.Add("prm_prmt_rqst_status_area_office", entity.StatusAreaOfficeCode is null ? DBNull.Value : entity.StatusAreaOfficeCode);
            _queryParams.Add("prm_prmt_rqst_status_external", entity.StatusExternalCode is null ? DBNull.Value : entity.StatusExternalCode);

            //_queryParams.Add("prm_prmt_rqst_status_approved", "AA");
            //_queryParams.Add("prm_prmt_rqst_status_pending", "PP");



            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
		}

		// ---------------------------------------------------------------------------------------------
		//                  Get all records (for use with PermitRequestViewModel).
		// ---------------------------------------------------------------------------------------------
		public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("PRMT_RQST_ID AS Id, ");
            _strQuery.Append("CONCAT (PRMT_RQST_LAST_NM, ', ', PRMT_RQST_FRST_NM) AS RequestorFullName, ");
            _strQuery.Append("PRMT_RQST_SUBMIT_DT AS DateCreated, ");
            _strQuery.Append("PRMT_RQST_SUBMIT_DT AS DateUpdated, ");
            _strQuery.Append("PRMT_RQST_STAT_TRAFF_CD AS StatusTrafficCode, ");
			_strQuery.Append("T1.PRMT_RQST_STAT_NM AS StatusTrafficName, ");
			_strQuery.Append("PRMT_RQST_STAT_TPD_CD AS StatusTPDCode, ");
			_strQuery.Append("T2.PRMT_RQST_STAT_NM AS StatusTPDName, ");
			_strQuery.Append("PRMT_RQST_STAT_AREA_OFFC_CD AS StatusAreaOfficeCode, ");
			_strQuery.Append("T3.PRMT_RQST_STAT_NM AS StatusAreaOfficeName, ");
            _strQuery.Append("PRMT_RQST_STAT_EXT_CD AS StatusExternalCode, ");
            _strQuery.Append("T4.PRMT_RQST_STAT_NM AS StatusExternalName, ");
            _strQuery.Append("PRMT_RQST_REQ_TRAFF AS RequiresTraffic, ");
            _strQuery.Append("PRMT_RQST_REQ_TPD AS RequiresTPD ");
            _strQuery.Append("FROM PRMT_RQST ");
			_strQuery.Append("LEFT JOIN PRMT_RQST_STAT T1 ON T1.PRMT_RQST_STAT_CD = PRMT_RQST.PRMT_RQST_STAT_TRAFF_CD ");
			_strQuery.Append("LEFT JOIN PRMT_RQST_STAT T2 ON T2.PRMT_RQST_STAT_CD = PRMT_RQST.PRMT_RQST_STAT_TPD_CD ");
			_strQuery.Append("LEFT JOIN PRMT_RQST_STAT T3 ON T3.PRMT_RQST_STAT_CD = PRMT_RQST.PRMT_RQST_STAT_AREA_OFFC_CD ");
            _strQuery.Append("LEFT JOIN PRMT_RQST_STAT T4 ON T4.PRMT_RQST_STAT_CD = PRMT_RQST.PRMT_RQST_STAT_EXT_CD ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("PRMT_RQST_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("RequestorFullName ");
            _strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all information from one or more records.
        // ---------------------------------------------------------------------------------------------
        public string ReadPermitRequests(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("PRMT_RQST.PRMT_RQST_ID AS PermitRequestId, ");
            _strQuery.Append("PRMT_RQST.HWY_ID AS HighwayId, ");
			_strQuery.Append("HWY_NBR AS HighwayNumber, ");
			_strQuery.Append("PRMT_RQST_SUBMIT_DT AS SubmitDate, ");
            _strQuery.Append("PRMT_RQST.CNSTR_TYPE_ID AS ConstructionTypeId, ");
			_strQuery.Append("CNSTR_TYPE_NM AS ConstructionTypeName, ");
			_strQuery.Append("PRMT_RQST.HWY_PRFX_ID AS HighwayPrefixId, ");
			_strQuery.Append("HWY_PRFX_CD AS HighwayCode, ");
			_strQuery.Append("PRMT_RQST_TYPE AS RequestType, ");
            _strQuery.Append("PRMT_RQST.CNTY_ID AS CountyId, ");
			_strQuery.Append("CNTY_NM AS CountyName, ");
			_strQuery.Append("PRMT_RQST.DIST_ID AS DistrictId, ");
			_strQuery.Append("DIST_NM AS DistrictName, ");
			_strQuery.Append("PRMT_RQST_FRST_NM AS RequestorFirstName, ");
            _strQuery.Append("PRMT_RQST_LAST_NM AS RequestorLastName, ");
            _strQuery.Append("PRMT_RQST_ADDR AS RequestorAddress, ");
            _strQuery.Append("PRMT_RQST_CITY_NM AS RequestorCity, ");
            _strQuery.Append("PRMT_RQST_ZIPCD AS RequestorZipCode, ");
            _strQuery.Append("PRMT_RQST_STATE_CD AS RequestorState, ");
            _strQuery.Append("PRMT_RQST_PHN_NBR AS RequestorPhoneNumber, ");
            _strQuery.Append("PRMT_RQST_LON AS Longitude, ");
            _strQuery.Append("PRMT_RQST_LAT AS Latitude, ");
			_strQuery.Append("PRMT_RQST_CNSTR_STRT_DT AS ConstructionStartDate, ");
			_strQuery.Append("PRMT_RQST_LITIG_FLAG AS LitigationFlag, ");
            _strQuery.Append("PRMT_RQST_CNSTR_FLAG AS ConstructionFlag, ");
            _strQuery.Append("PRMT_RQST_STATE_REP_NM AS StateRepresentativeName, ");
            _strQuery.Append("PRMT_RQST_STATE_REP_PHN_NBR AS StateRepresentativePhoneNumber, ");
            _strQuery.Append("PRMT_RQST_OTHR_COND_TXT AS OtherConditions, ");
            _strQuery.Append("PRMT_RQST_VARNC_ONE_FLAG AS VarianceOneFlag, ");
            _strQuery.Append("PRMT_RQST_VARNC_TWO_FLAG AS VarianceTwoFlag, ");
            _strQuery.Append("PRMT_RQST_VARNC_JUST_TXT AS VarianceJustification, ");
            _strQuery.Append("PRMT_RQST_VARNC_DENL_ONE_FLAG AS VarianceDenialOneFlag, ");
            _strQuery.Append("PRMT_RQST_VARNC_DENL_TWO_FLAG AS VarianceDenialTwoFlag, ");
            _strQuery.Append("PRMT_RQST_REQ_TRAFF AS RequiresTraffic, ");
            _strQuery.Append("PRMT_RQST_REQ_TPD AS RequiresTPD, ");

            _strQuery.Append("RES_SKCH_ID AS PermitRequestResidentialId, ");
            _strQuery.Append("RES_SKCH_PIPE_LNGTH_MS AS PipeLength, ");
            _strQuery.Append("RES_SKCH_DWAY_WIDTH_MS AS DrivewayWidth, ");
            _strQuery.Append("RES_SKCH_DSTNCE_TO_CNTR_MS AS DistanceToCenter, ");
            _strQuery.Append("RES_SKCH_DSTNCE_FROM_EDGE_MS AS DistanceFromEdge, ");
            _strQuery.Append("RES_SKCH_RADIUS_ONE_MS AS RadiusOne, ");
            _strQuery.Append("RES_SKCH_RADIUS_TWO_MS AS RadiusTwo, ");
            _strQuery.Append("RES_SKCH_DITCH_DEPTH_MS AS DrainagePipe, ");
            _strQuery.Append("RES_SKCH_PIPE_SZ_MS AS DrainageStructure, ");
            _strQuery.Append("RES_SKCH_SET_BACK_WIDTH_GATE_MS AS WidthGate, ");
            _strQuery.Append("RES_SKCH_SET_BACK_WIDTH_ROW_MS AS WidthROW, ");
            _strQuery.Append("RES_SKCH_THROAT_LNGTH_MS AS ThroatLength, ");

			_strQuery.Append("ISNULL");
			_strQuery.Append("((SELECT ");
			_strQuery.Append("ATCHMT_ID AS AttachmentId, ");
			_strQuery.Append("ATCHMT_NM AS AttachmentName, ");
			_strQuery.Append("Attachments.ATCHMT_TYPE_ID AS AttachmentTypeId, ");
			_strQuery.Append("ATCHMT_TYPE_NM AS AttachmentTypeName, ");
			_strQuery.Append("ATCHMT_CMT AS AttachmentComment ");
			_strQuery.Append("FROM ATCHMT AS Attachments ");
            _strQuery.Append("INNER JOIN ATCHMT_TYPE AS AttachmentType ON Attachments.ATCHMT_TYPE_ID = AttachmentType.ATCHMT_TYPE_ID ");
			_strQuery.Append("WHERE Attachments.PRMT_RQST_ID = PRMT_RQST.PRMT_RQST_ID ");
			_strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES ");
			_strQuery.Append("), '[]') AS Attachments ");

			_strQuery.Append("FROM PRMT_RQST ");
            _strQuery.Append("INNER JOIN RES_SKCH AS Sketch ON PRMT_RQST.PRMT_RQST_ID = Sketch.PRMT_RQST_ID ");
			_strQuery.Append("INNER JOIN CNSTR_TYPE AS ConstructionType ON PRMT_RQST.CNSTR_TYPE_ID = ConstructionType.CNSTR_TYPE_ID ");
			_strQuery.Append("INNER JOIN HWY_PRFX AS HighwayPrefix ON PRMT_RQST.HWY_PRFX_ID = HighwayPrefix.HWY_PRFX_ID ");
			_strQuery.Append("INNER JOIN HWY AS HighwayNumber ON PRMT_RQST.HWY_ID = HighwayNumber.HWY_ID ");
			_strQuery.Append("INNER JOIN CNTY AS County ON PRMT_RQST.CNTY_ID = County.CNTY_ID ");
			_strQuery.Append("INNER JOIN DIST AS District ON PRMT_RQST.DIST_ID = District.DIST_ID ");

			// A record with specific "id" is searched.
			if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("PRMT_RQST.PRMT_RQST_ID = @prm_id ");
            }

            _strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Read attachments for a specific permit request.
        // ---------------------------------------------------------------------------------------------
        public IEnumerable<AttachmentHelperModel> ReadAttachments(int? id = -1)
        {
			_strQuery.Clear();

			_strQuery.Append("SELECT ");
			_strQuery.Append("ATCHMT_ID AS Id, ");
			_strQuery.Append("ATCHMT_NM AS Name, ");
			_strQuery.Append("ATCHMT_TYPE_ID AS AttachmentTypeId, ");
			_strQuery.Append("ATCHMT_CMT AS Comment ");
			_strQuery.Append("FROM ATCHMT ");

			// A record with specific "id" is searched.
			if (id != -1)
			{
				_strQuery.Append("WHERE ");
				_strQuery.Append("PRMT_RQST_ID = @prm_id ");
			}

			_strQuery.Append("ORDER BY ");
			_strQuery.Append("AttachmentTypeId ");
			_strQuery.Append("FOR JSON PATH, INCLUDE_NULL_VALUES");

			// A record with specific "id" is searched.
			_queryParams.Clear();
			if (id != -1)
			{
				_queryParams.Add("prm_id", id);
			}

			string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);
            var attachments = (IEnumerable<AttachmentHelperModel>) JsonSerializer.Deserialize<AttachmentHelperModel>(result);

			return attachments;
        }

		// ---------------------------------------------------------------------------------------------
		//        Release database resources.       
		// ---------------------------------------------------------------------------------------------
		public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }

        // ---------------------------------------------------------------------------------------------
        //               Convert to upper case specific fields before CRUD operation.
        // ---------------------------------------------------------------------------------------------
        private static void ConvertCase(PermitRequestResidentialHelperModel entity)
        {
            //entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
        }
    }
}
