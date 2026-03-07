
namespace Util
{
    using UI;
    using UnityEngine;
    public class Portrait : MonoBehaviour
    {
        public void HurtPortraitStart()
        {
            GetComponent<UIIdleRotate>().enabled = true;
        }
        public void HurtPortraitEnd()
        {
            GetComponent<UIIdleRotate>().enabled = false;
            GetComponent<Animator>().Play("PortraitIdle");

        }
    }
}