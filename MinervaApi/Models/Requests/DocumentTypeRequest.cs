﻿namespace Minerva.Models.Requests
{
    public class DocumentTypeRequest
    {
        public int DocumentTypeAutoId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TenantId { get; set; }
        public string? DocumentTypeName { get; set; }
        public string? DocumentTypeDescription { get; set; }
        public int? DocumentClassificationId { get; set; }
        public string? TemplateFilePath { get; set; }

    }
}
