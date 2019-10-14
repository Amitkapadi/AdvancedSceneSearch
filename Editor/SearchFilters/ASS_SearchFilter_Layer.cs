using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

namespace KeaneGames.AdvancedSceneSearch
{
    [Serializable]
    public class ASS_SearchFilter_Layer : ASS_SearchFilter
    {
        [SerializeField]
        private int _layerMask = ~0;

        public ASS_SearchFilter_Layer() : base("Layer")
        {
        }

        public override void Reset()
        {
            _layerMask = ~0;

            base.Reset();
        }

        public override void DrawSearchGui()
        {
            _layerMask = EditorGUIExtensions.LayerMaskField("Layers:", _layerMask);

            base.DrawSearchGui();
        }

        public override bool Actionable
        {
            get { return !(_layerMask == ~0 || _layerMask == 0); }
        }


        public override IEnumerable<GameObject> ApplyFilter(IEnumerable<GameObject> selectedObjs)
        {
            if(Actionable)
                selectedObjs = selectedObjs.Where(x => _layerMask == (_layerMask | (1 << x.layer)));

            return selectedObjs;
        }

        public override string GetFilterText()
        {
            if (!Actionable)
                return "";

            return "Specific layers set" + Environment.NewLine;

        }
    }
}