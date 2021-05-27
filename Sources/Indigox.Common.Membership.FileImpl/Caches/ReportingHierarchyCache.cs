using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.FileImpl.Caches
{
    internal class ReportingHierarchyCache
    {
        private static Dictionary<int, Dictionary<string, string>> idToNodeMap = new Dictionary<int, Dictionary<string, string>>();
        private static Dictionary<int, ReportingHierarchy> idToHierarchyMap = new Dictionary<int, ReportingHierarchy>();

        static ReportingHierarchyCache()
        {
            AddReportingHierarchys();
            AddReportingHierarchyUsers();
        }

        private static void AddReportingHierarchys()
        {
            idToHierarchyMap.Add( 1, new ReportingHierarchy() { ID = 1, Name = "默认汇报关系树" } );
            idToHierarchyMap.Add( 2, new ReportingHierarchy() { ID = 2, Name = "Other" } );
        }

        private static void AddReportingHierarchyUsers()
        {
            using ( CsvFileReader reader = new CsvFileReader( @".\data\reportinghierarchyusers.csv" ) )
            {
                string[] cols = reader.Read();

                while ( cols != null )
                {
                    int id = int.Parse( cols[ reader.GetOrdinal( "ReportingHierarchyID" ) ] );
                    string user = cols[ reader.GetOrdinal( "UserID" ) ];
                    string manager = cols[ reader.GetOrdinal( "ManagerID" ) ];
                    if ( !idToNodeMap.ContainsKey( id ) )
                    {
                        idToNodeMap.Add( 1, new Dictionary<string, string>() );
                    }
                    idToNodeMap[ id ].Add( user, manager );

                    cols = reader.Read();
                }
            }
        }

        public static Dictionary<string, string> GetNodes( IReportingHierarchy hierarchy )
        {
            if ( !idToNodeMap.ContainsKey( hierarchy.ID ) )
            {
                idToNodeMap.Add( hierarchy.ID, new Dictionary<string, string>() );
            }
            Dictionary<string, string> nodes = idToNodeMap[ hierarchy.ID ];
            return nodes;
        }

        public static ReportingHierarchy GetReportingHierarchyById( int id )
        {
            if ( idToHierarchyMap.ContainsKey( id ) )
            {
                return idToHierarchyMap[ id ];
            }
            return null;
        }

        public static IList<IReportingHierarchy> GetAllReportingHierarchy()
        {
            List<IReportingHierarchy> list = new List<IReportingHierarchy>();
            foreach ( var item in idToHierarchyMap.Values )
            {
                list.Add( item );
            }
            return list;
        }
    }
}