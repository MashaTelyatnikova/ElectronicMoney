package ru.kontur.crypto.money;

import sun.reflect.generics.reflectiveObjects.NotImplementedException;

import java.util.List;
import java.util.Random;

public class Bank {
    public int[] getChequeSelector(int n){
        int[] tmp = new int[n];
        for (int i = 0; i < n; ++i){
            tmp[i] = i + 1;
        }

        shuffleArray(tmp);
        int[] result = new int[n - 1];
        for (int i = 0; i < n - 1; ++i){
            result[i] = tmp[i];
        }

        return result;
    }

    private static void shuffleArray(int[] array) {
        Random rnd = new Random(System.currentTimeMillis());
        for (int i = array.length - 1; i > 0; i--)
        {
            int index = rnd.nextInt(i + 1);
            int a = array[index];
            array[index] = array[i];
            array[i] = a;
        }
    }

    public BankSignature getSignature(){
        throw new NotImplementedException();
    }

    public boolean checkEnvelopes(List<Envelope> envelopes){
        throw new NotImplementedException();
    }

    public boolean checkCheque(Cheque cheque){
        throw new NotImplementedException();
    }
}
