using UnityEngine;

public interface Interactable
{
     void Interaction();

     // Controla el ícono/feedback de "Presiona E" mientras el jugador está en rango.
    void ShowPrompt();
    void HidePrompt();

}
