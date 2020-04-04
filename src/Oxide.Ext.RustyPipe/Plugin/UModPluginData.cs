using System;
using System.Collections.Generic;

namespace Oxide.Ext.RustyPipe.Plugin
{
    public class UModPluginData
    {
        public string version { get; set; }
        public string description { get; set; }
        public int visible { get; set; }
        public string checksum { get; set; }
        public object tags { get; set; }
        public int downloads { get; set; }
        public string created_at { get; set; }
        public string description_md { get; set; }
        public string download_url { get; set; }
        public string revert_url { get; set; }
        public string toggle_url { get; set; }
        public string delete_url { get; set; }
        public string edit_url { get; set; }
        public bool is_latest { get; set; }
        public string text_class { get; set; }
        public bool revertable { get; set; }
        public string toggle_icon { get; set; }
        public string version_formatted { get; set; }
        public string build_ids { get; set; }
        public string game_versions { get; set; }
        public string game_protocols { get; set; }
        public object version_branches { get; set; }
        public string downloads_shortened { get; set; }
        public string downloads_lang { get; set; }
        public DateTime created_at_atom { get; set; }
        public DateTime updated_at_atom { get; set; }
        public IList<object> steam_builds { get; set; }
    }
}
