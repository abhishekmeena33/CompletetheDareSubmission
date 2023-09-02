using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<PlayerMovement> inContact;
    public List<PlayerMovement> currentActive;

    
    void Start()
    {
        inContact = new List<PlayerMovement>();
        currentActive = new List<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {
            List<PlayerMovement> offLis =  inContact.Where(x => !x.isActiveForMovement).ToList();
            foreach(PlayerMovement x in offLis) {
                x.isActiveForMovement = true;
                currentActive.Add(x);
            }
        }

        if(Input.GetKey(KeyCode.RightShift)) {
            List<PlayerMovement> toRemoveList = new List<PlayerMovement>();
            foreach(PlayerMovement x in currentActive) {
                x.isActiveForMovement = false;
                if(x.hasSpirit) {
                    x.isActiveForMovement = true;
                } else {
                    toRemoveList.Add(x);
                }
            }

            foreach(PlayerMovement x in toRemoveList) {
                currentActive.Remove(x);
            }
        }

        spiritTransfer();
    }

    Vector2 spiritTransferInput() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            return Vector2.left;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            return Vector2.right;
        } else if(Input.GetKey(KeyCode.UpArrow)) {
            return Vector2.up;
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            return Vector2.down;
        } else {
            return Vector2.zero;
        }
    }
    void spiritTransfer() {
        Vector2 inp = spiritTransferInput();
        if(inp != Vector2.zero) {
            PlayerMovement curSpirit = currentActive.Find(x => x.hasSpirit);
            
            foreach(PlayerMovement x in currentActive) {
                
                Vector3 tempV = (x.transform.position - curSpirit.transform.position);
                Vector2 temp2 = new Vector2(tempV.x, tempV.y);
                if(Vector2.Dot(temp2, inp) > 0.0f && Vector2.Dot(temp2, inp) < 1.5f){
                    
                    x.setSpirit(true);
                    curSpirit.setSpirit(false);
                    break;
                }
            }
        }
    }

}
