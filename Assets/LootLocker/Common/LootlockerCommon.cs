using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker;
using System.Linq;
using UnityEngine.UI;

namespace LootLocker
{
    public class Links
    {
        public string thumbnail { get; set; }
    }

    public class Default_Loadouts_Info
    {
        public bool Default { get; set; }
    }

    public class Variation_Info
    {
        public int id { get; set; }
        public string name { get; set; }
        public object primary_color { get; set; }
        public object secondary_color { get; set; }
        public object links { get; set; }
    }

    [System.Serializable]
    public class AssetRequest : LootLockerResponse
    {
        public int count;
        public static int lastId;
    }

    public class AssetResponse : LootLockerResponse
    {
        public bool success { get; set; }
        public Asset[] assets { get; set; }
    }

    public class Rental_Options
    {
        public int id { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
        public int price { get; set; }
        public object sales_price { get; set; }
        public object links { get; set; }
    }

    public class Asset
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public bool active { get; set; }
        public bool purchasable { get; set; }
        public bool tradable { get; set; }
        public bool marketable { get; set; }
        public string name { get; set; }
        public string context { get; set; }
        public string last_changed { get; set; }
        public int price { get; set; }
        public int sales_price { get; set; }
        public List<Filter> filters { get; set; }
        public string thumbnail { get; set; }
        public List<Variation> variations { get; set; }
        public object rental_options { get; set; } 
        public object storage { get; set; }
        public object flagged { get; set; }
        // public int id { get; set; }
        // public string name { get; set; }
        // public bool active { get; set; }
        // public bool purchasable { get; set; }
        // public string type { get; set; }
        // public int price { get; set; }
        // public string sales_price { get; set; }
        // public string display_price { get; set; }
        // public string context { get; set; }
        // public string unlocks_context { get; set; }
         public bool detachable { get; set; }
        // public string updated { get; set; }
        // public string marked_new { get; set; }
         public int default_variation_id { get; set; }
        // public string description { get; set; }
        // public Links links { get; set; }
        // public string[] storage { get; set; }
        // public string rarity { get; set; }
        // public bool popular { get; set; }
        // public int popularity_score { get; set; }
        // public bool unique_instance { get; set; }
        // public string external_identifiers { get; set; }
        // public Rental_Options[] rental_options { get; set; }
        // public string[] filters { get; set; }
        // public Variation[] variations { get; set; }
        // public bool featured { get; set; }
        // public bool context_locked { get; set; }
        // public bool initially_purchasable { get; set; }
    }
    public class Filter
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class File
    {
        public string url { get; set; }
        public string[] tags { get; set; }
    }


    public class Default_Loadouts
    {
        public bool Default { get; set; }
    }

    public class Variation
    {
        public int id { get; set; }
        public string name { get; set; }
        public object primary_color { get; set; }
        public object secondary_color { get; set; }
        public object links { get; set; }
    }

}