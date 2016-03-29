using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.Characters.ThirdPerson {
    public class thirdpersonview : MonoBehaviour {
        public float movespeed = 2.0f;
        public ThirdPersonCharacter m_char;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            float forwardspeed = Input.GetAxis("Vertical") * movespeed;
            // float sidestep = 7.5f;
            Vector3 speed = new Vector3(0, 0, forwardspeed);

            CharacterController cc = GetComponent<CharacterController>();
            //cc.SimpleMove(speed);
            m_char.Move(speed, false, false);
        }
    }
}