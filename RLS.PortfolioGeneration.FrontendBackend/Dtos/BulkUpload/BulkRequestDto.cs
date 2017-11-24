using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos.BulkUpload
{
    public class BulkRequestDto
    {
        public string RequestId { get; set; }

        public string PortfolioId { get; set; }
        
        public AccountDto Account { get; set; }

        public QualifiedSiteDto[] Sites { get; set; }
    }

    public class QualifiedSiteDto : SiteDto
    {
        public MpanDto[] Mpans { get; set; }

        public MprnDto[] Mprns { get; set; }
    }

    public class BulkResponseDto
    {
        public string RequestId { get; set; }

        public string PortfolioId { get; set; }
        
        public BulkUploadResponseDto Account { get; set; }

        public BulkUploadSitesResponseDto Sites { get; set; }
    }

    public class BulkUploadResponseDto
    {
        public Guid Id { get; set; }

        public string State { get; set; }
    }

    public class BulkUploadSitesResponseDto : BulkUploadResponseDto
    {
        public string SiteCode { get; set; }

        public List<MeterBulkUploadResponseDto> Mpans { get; set; }

        public List<MeterBulkUploadResponseDto> Mprns { get; set; }
    }

    public class MeterBulkUploadResponseDto : BulkUploadResponseDto
    {
        public string Core { get; set; }
    }

    public static class BulkUploadResponseStates
    {
        public static readonly string Created = "Created";
        public static readonly string Updated = "Updated";
        public static readonly string Errored = "Errored";
    }
}