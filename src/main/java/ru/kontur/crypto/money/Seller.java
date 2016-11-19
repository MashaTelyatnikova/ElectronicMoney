package ru.kontur.crypto.money;

import java.util.Random;

public class Seller {
    public boolean[] generateSelector(int n){
        boolean[] result = new boolean[n];
        Random random = new Random(System.currentTimeMillis());

        for (int i = 0; i < n; ++i){
            result[i] = random.nextBoolean();
        }

        return result;
    }
}
