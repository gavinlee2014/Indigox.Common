using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Indigox.Common.Expression.Interface
{
    public interface IPropertySet
    {
        /// <summary>
        /// properties count
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// get property value. if property not exists throw an KeyNotFoundException
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="KeyNotFoundException">The property is retrieved and key does not exist in the set.</exception>
        /// <returns></returns>
        object Get( string name );

        /// <summary>
        /// add property. if property exists throw an ArgumentException
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException">An element with the same key already exists in the set.</exception>
        void Add( string name, object value );

        /// <summary>
        /// if property exists, then set the value. else add property.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Set( string name, object value );

        /// <summary>
        /// remove property. if property not exists throw an KeyNotFoundException
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="KeyNotFoundException">The property is retrieved and key does not exist in the set.</exception>
        /// <returns></returns>
        void Remove( string name );

        /// <summary>
        /// clear all properties
        /// </summary>
        void Clear();

        /// <summary>
        /// if contains property return true, else return false.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Contains( string name );
    }
}
