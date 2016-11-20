package ru.kontur.crypto.money;

import java.math.BigInteger;
import java.util.Random;

public class Bank {
    private static final long p = 17;
    private static final long q = 31;
    private static final long n = p * q;
    private static final long e = 7;
    private static final long d = 343;

    public long getN() {
        return n;
    }

    public long getE() {
        return e;
    }

    public long sign(long m) {
        return BigInteger.valueOf(m).modPow(BigInteger.valueOf(d), BigInteger.valueOf(n)).longValue();
    }

    public int[] getChequeSelector(int n) {
        int[] tmp = new int[n];
        for (int i = 0; i < n; ++i) {
            tmp[i] = i + 1;
        }

        shuffleArray(tmp);
        int[] result = new int[n - 1];
        for (int i = 0; i < n - 1; ++i) {
            result[i] = tmp[i];
        }

        return result;
    }

    private static void shuffleArray(int[] array) {
        Random rnd = new Random(System.currentTimeMillis());
        for (int i = array.length - 1; i > 0; i--) {
            int index = rnd.nextInt(i + 1);
            int a = array[index];
            array[index] = array[i];
            array[i] = a;
        }
    }

    //todo if ok sub sum and give moey, save id
    public SignatureCheckingResult checkChequeSignature(Cheque cheque) {
        long s1 = sign(new BigInteger(cheque.getContent()).mod(BigInteger.valueOf(n)).longValue());

        if (s1 == cheque.getSignature()) {
            return SignatureCheckingResult.OK;
        }

        return SignatureCheckingResult.WRONG;
    }
}
