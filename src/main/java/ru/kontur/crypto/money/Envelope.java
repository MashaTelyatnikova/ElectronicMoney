package ru.kontur.crypto.money;

import java.math.BigInteger;

public class Envelope {
    private Cheque cheque;
    private final long k;

    public Envelope(Cheque cheque){
        this.cheque = cheque;
        this.k = System.currentTimeMillis();
    }

    public void signCheque(BankSignature bank){
        BigInteger m = new BigInteger(cheque.getContent());
        BigInteger k1 = BigInteger.valueOf(k);
        BigInteger d = BigInteger.valueOf(bank.getPublicKey());
        BigInteger p = BigInteger.valueOf(bank.getP());

        BigInteger res = m.multiply(k1).modPow(d, p);
        BigInteger k2 = k1.modPow(d, p);
        BigInteger signature = res.divide(k2);
        cheque.setSignature(signature);
    }
}
