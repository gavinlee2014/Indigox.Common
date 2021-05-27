using System;
using System.Collections;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Repository
{
    class BasicRepositoryCache : Hashtable
    {
        private static BasicRepositoryCache instance;

        private BasicRepositoryCache()
        {
        }

        public static BasicRepositoryCache Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new BasicRepositoryCache();
                }
                return instance;
            }
        }
    }
}
