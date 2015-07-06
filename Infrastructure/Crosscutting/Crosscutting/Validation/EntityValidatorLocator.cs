
//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


namespace Swaksoft.Infrastructure.Crosscutting.Validation
{
    /// <summary>
    /// Entity Validator Factory
    /// </summary>
    public static class EntityValidatorLocator
    {
        static IEntityValidatorFactory _factory;
        private static readonly object _thisObject = new object();
        
        /// <summary>
        /// Set the  log factory to use
        /// </summary>
        /// <param name="factory">Log factory to use</param>
        public static void SetCurrent(IEntityValidatorFactory factory)
        {
            lock (_thisObject)
            {
                _factory = factory;
            }            
        }

        /// <summary>
        /// Createt a new <paramref />
        /// </summary>
        /// <returns>Created ILog</returns>
        public static IEntityValidator CreateValidator()
        {
            return (_factory != null) ? _factory.Create() : null;
        }
    }
}
