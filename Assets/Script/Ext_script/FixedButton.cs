using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour,IPointerClickHandler
{
    public playerMovementScript player;

    public void SetPlayer(playerMovementScript _player)
    {
        player = _player;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //player.Jump();
    }

  

   
}
