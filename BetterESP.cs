using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC;

[assembly: MelonInfo(typeof(KiraiMod.BetterESP), "BetterESP", "0.1.0", "xKiraiChan", "github.com/xKiraiChan/BetterESP")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace KiraiMod
{
    public class BetterESP : MelonMod
    {
        public AssetBundle resources;
        public List<Object> created = new List<Object>();

        public override void OnApplicationStart()
        {
            var a = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("KiraiMod.BetterESP.assetbundle");
            var b = new System.IO.MemoryStream((int)a.Length);
            a.CopyTo(b);

            resources = AssetBundle.LoadFromMemory_Internal(b.ToArray(), 0);
            resources.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (Object obj in created)
                    if (obj != null)
                        Object.Destroy(obj);
                created.Clear();

                var mat = resources.LoadAsset_Internal("assets/betteresp.mat", Il2CppType.Of<Material>()).Cast<Material>();

                foreach (var p in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
                {
                    var a = p.transform.Find("ForwardDirection/Avatar");

                    var b = a.GetComponentsInChildren<SkinnedMeshRenderer>();
                    for (int i = 0; i < b.Length; i++)
                    {
                        var d = Object.Instantiate(b[i], b[i].transform.parent);
                        created.Add(d);
                        d.materials = Process(d.materials, mat);
                    }

                    var c = a.GetComponentsInChildren<MeshRenderer>();
                    for (int i = 0; i < c.Length; i++)
                    {
                        var d = Object.Instantiate(c[i], c[i].transform.parent);
                        created.Add(d);
                        d.materials = Process(d.materials, mat);
                    }
                }
            }
        }

        public Il2CppReferenceArray<Material> Process(Il2CppReferenceArray<Material> mats, Material mat)
        {
            for (int i = 0; i < mats.Length; i++)
                mats[i] = mat;
            return mats;
        }
    }
}
