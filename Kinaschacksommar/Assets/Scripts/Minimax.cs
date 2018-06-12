using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Kai-Mikael Jää-Aaro (redigerad av Timmy Alvelöv & Andreas de Freitas)
public class Minimax : MonoBehaviour
{
    public State BubbleDown(State state, Player player, Player opponent, int depth, bool max)
    {
        State nextState = state;
        state.currentValue = state.Value(player);
        if (depth == 0 || state.currentValue == System.Int32.MaxValue || state.currentValue == System.Int32.MinValue) // Vi vill inte räkna vidare efter någon av dessa är uppfyllda, då vi antingen vunnit eller förlorat då (för två spelare i alla fall)
            return state;

        State child; // En temporär variabel för att kolla vad nästa steg ska bli
        if (max) // Ska vi maxa eller mina 
        {
            state.currentValue = System.Int32.MinValue; // Sätter statets värde till det minsta möjliga värdet en (32-bitars) int kan ta
            nextState = null;
            List<State> Children = state.Expand(player); // Children får värdet av alla möjliga moves statet state har
            foreach (State s in Children)
            {
                child = BubbleDown(s, opponent, player, depth - 1, false); // Går ett steg djupare
                if (child != null && child.currentValue >= state.currentValue) //Om det nya statet har ett högre värde
                {
                    nextState = s; //Spara statet
                    state.currentValue = child.currentValue; // Spara värdet på statet till framtida jämförelser
                }
            }
        }
        else // Om vi inte maxar (A.K.A Minar)
        {
            state.currentValue = System.Int32.MaxValue;
            nextState = null;
            List<State> Children = state.Expand(opponent);
            foreach (State s in Children)
            {
                child = BubbleDown(s, opponent, player, depth - 1, true);
                if (child != null && child.currentValue <= state.currentValue)
                {
                    nextState = s;
                    state.currentValue = child.currentValue;
                }
            }

        }
        return (nextState); // Lämnar tillbaka statet med bäst värde.
    }
}
