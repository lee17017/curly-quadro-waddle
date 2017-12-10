using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager {
    public static float[] scores = new float[4];
    public static int[] ranking = new int[4];
    public static void reset() {
        for (int i = 0; i < 4; i++)
        {
            scores[i] = 0;
            ranking[i] = -1;
        }
            
    }

    public static void addScore(int player, float score) {
        scores[player - 1] += score;
    }

    public static void calculateRanking(){
        for (int i = 0; i < 4; i++) {
            int rank = 1;
            for(int j=0; j<4; j++)
            {
                if (i!=j && scores[i] < scores[j])
                    rank++;
            }
            ranking[i] = rank;
        }
    }

    public static int getRank(int player)
    {
        return ranking[player];
    }

    public static float getHighestScore() {
        float value = scores[0];
        for(int i=1; i < 4; i++)
        {
            if (value < scores[i])
                value = scores[i];
        }
        return value;
    }

    public static int getWinner()
    {
        int pointer = 0;
        for (int i = 1; i < 4; i++)
        {
            if (scores[pointer] < scores[i])
                pointer = i;
        }
        return pointer+1;
    }

    public static bool draw()
    {
        int winner = getWinner()-1;
        for (int i = winner+1; i < 4; i++) {
            if(scores[winner] <= scores[i])
            {
                return true;
            }
        }
        return false;
    }



}
