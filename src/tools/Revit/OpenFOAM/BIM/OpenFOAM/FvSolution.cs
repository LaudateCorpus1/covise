﻿/* This file is part of COVISE.

   You can use it under the terms of the GNU Lesser General Public License
   version 2.1 or later, see lgpl-2.1.txt.

 * License: LGPL 2+ */
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace OpenFOAMInterface.BIM.OpenFOAM
{
    /// <summary>
    /// This class is represantive for the fvSolution-Dictionary in the system folder of the openFOAM-case-folder.
    /// </summary>
    public class FvSolution : FOAMDict
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">Version-object.</param>
        /// <param name="path">Path to this file.</param>
        /// <param name="attributes">Additional attributes.</param>
        /// <param name="format">Ascii or Binary</param>
        /// <param name="settings">Settings-object</param>
        public FvSolution(Version version, string path, Dictionary<string, object> attributes, SaveFormat format)
            : base("fvSolution", "dictionary", version, path, attributes, format)
        {
            InitAttributes();
        }

        private System.Windows.Media.Media3D.Vector3D ConvertToDisplayUnits(System.Windows.Media.Media3D.Vector3D v)
        {
            return new System.Windows.Media.Media3D.Vector3D(UnitUtils.ConvertFromInternalUnits(v.X, UnitTypeId.Meters), UnitUtils.ConvertFromInternalUnits(v.Y, UnitTypeId.Meters), UnitUtils.ConvertFromInternalUnits(v.Z, UnitTypeId.Meters));
        }
        /// <summary>
        /// Initialize attributes of this file.
        /// </summary>
        public override void InitAttributes()
        {
            Dictionary<string, object> m_SIMPLE = m_DictFile["SIMPLE"] as Dictionary<string, object>;

            object val = null;
            if (m_SIMPLE.TryGetValue("pRefPoint", out val) == true)
            {
                m_SIMPLE["pRefPoint"] = ConvertToDisplayUnits(Exporter.Instance.settings.LocationInMesh);
            }
            else
            {
                m_SIMPLE.Add("pRefPoint", ConvertToDisplayUnits(Exporter.Instance.settings.LocationInMesh)/*"(" + BIM.OpenFOAMExport.Exporter.Instance.settings.LocationInMesh.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat).Replace(';', ' ') + ")"*/);
            }
            base.InitAttributes();
        }
    }
}
