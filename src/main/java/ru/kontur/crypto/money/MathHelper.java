package ru.kontur.crypto.money;

public class MathHelper {
    public static long Mode(long a, long p){
        long result = a % p;
        return result < 0 ? result + p : result;
    }
}
