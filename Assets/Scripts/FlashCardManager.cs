using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCardManager
{
    public FlashCard[] questions;

    public FlashCardManager() {
        questions = new FlashCard[3];
        questions[0] = new FlashCard(
            "The intelligence of machines and the branch of computer science that aims to create it.",
            "Artificial Intelligence", "Rational Agent", "Turing Test",
            0
        );
        questions[1] = new FlashCard(
            "Within artificial intelligence, a __________ is one that maximizes its expected utility, given its current knowledge.",
            "Artificial Intelligence", "Rational Agent", "Turing Test",
            1
        );
        questions[2] = new FlashCard(
            "Which structure is not found in eukaryotic plant cell",
            "Golgi body", "Plasmids", "Mitochondia",
            1
        );
    }
}
