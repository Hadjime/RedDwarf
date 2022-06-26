using UnityEngine;


namespace InternalAssets.Scripts.Utils.StringInEnum
{
    public class TestAtribute: MonoBehaviour
    {
        private void Start()
        {
            StringEnum temp = StringEnum.Asdf;

            string stringValue = StringValueAttribute.GetStringValue(temp);
        }
    }
}
