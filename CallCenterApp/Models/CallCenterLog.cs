using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterApp.Models
{
    public class CallCenterLog
    {
        public float searchscore { get; set; }
        public string id { get; set; }
        public string audio_file { get; set; }
        public string metadata_storage_content_type { get; set; }
        public int metadata_storage_size { get; set; }
        public DateTimeOffset? metadata_storage_last_modified { get; set; }
        public string metadata_storage_name { get; set; }
        public string metadata_storage_path { get; set; }
        public string transcription_job { get; set; }
        public string language { get; set; }
        public double audio_length_in_seconds { get; set; }
        public object channel0 { get; set; }
        public object channel1 { get; set; }
        public Summary summary { get; set; }
        public Conversation[] conversation { get; set; }
    }

    public class Summary
    {
        public float max_change { get; set; }
        public int max_change_index { get; set; }
        public float max_change_time { get; set; }
        public float lowest_sentiment { get; set; }
        public float highest_sentiment { get; set; }
    }

    public class Conversation
    {
        public int? turn { get; set; }
        public string speaker { get; set; }
        public string text { get; set; }
        public long? offset { get; set; }
        public int? duration { get; set; }
        public float? offset_in_seconds { get; set; }
        public float? duration_in_seconds { get; set; }
        public float? sentiment { get; set; }
        public string[] locations { get; set; }
        public string[] organizations { get; set; }
        public string[] people { get; set; }
        public string[] key_phrases { get; set; }
        public DateTimeOffset? creation_date { get; set; }
    }
}

