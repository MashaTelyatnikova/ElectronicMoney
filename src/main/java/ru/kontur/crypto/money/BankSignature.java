package ru.kontur.crypto.money;

public class BankSignature {
    private long l = 100;
    private long d = 100;
    private long p = 3571;

    public long sign(long m){
        return (long) (Math.pow(m, l) % p);
    }

    public boolean check(long m, long s){
        return sign(m) == s;
    }
    public long getPublicKey() {
        return d;
    }

    public long getP() {
        return p;
    }
}
