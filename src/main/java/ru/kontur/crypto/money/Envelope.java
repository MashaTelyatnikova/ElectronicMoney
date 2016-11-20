package ru.kontur.crypto.money;

import java.math.BigInteger;

public class Envelope {
    private Cheque cheque;

    public Envelope(Cheque cheque){
        this.cheque = cheque;
    }

    public void setSignatureFrom(Bank bank){
        BigInteger k = null;
        while (true){
            long tmp = System.currentTimeMillis();
            if (gcd(tmp, bank.getN()) == 1){
                k = BigInteger.valueOf(tmp);
                break;
            }
        }
        BigInteger m = new BigInteger(cheque.getContent());
        BigInteger e = BigInteger.valueOf(bank.getE());
        BigInteger n = BigInteger.valueOf(bank.getN());

        BigInteger kExpE = k.modPow(e, n);
        BigInteger signedK = (BigInteger.valueOf(bank.sign(kExpE.longValue()))).modInverse(n);

        BigInteger content = (m.multiply(kExpE)).mod(n);
        long result = bank.sign(content.longValue());
        BigInteger signature = (BigInteger.valueOf(result).multiply(signedK)).mod(n);

        cheque.setSignature(signature.longValue());
    }

    private static long gcd (long a, long b){
        return b == 0 ? a : gcd(b, a % b);
    }
}
