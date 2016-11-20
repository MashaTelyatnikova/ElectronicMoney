package ru.kontur.crypto.money;

import java.security.MessageDigest;

public class Cheque {
    private String id;
    private int sum;
    private Identification[] identifications;
    private byte[] content;
    private long signature;

    public Cheque(String id, int sum) {
        String m1 = id + sum;

        try {
            byte[] bytesOfMessage = m1.getBytes("UTF-8");

            MessageDigest md = MessageDigest.getInstance("MD5");
            content = md.digest(bytesOfMessage);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public String[] select(boolean[] selector) {
        String[] result = new String[identifications.length];
        for (int i = 0; i < identifications.length; ++i) {
            result[i] = selector[i] ? identifications[i].getLeft() : identifications[i].getRight();
        }

        return result;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public int getSum() {
        return sum;
    }

    public void setSum(int sum) {
        this.sum = sum;
    }

    public Identification[] getIdentifications() {
        return identifications;
    }

    public void setIdentifications(Identification[] identifications) {
        this.identifications = identifications;
    }

    public byte[] getContent() {
        return content;
    }

    public long getSignature() {
        return signature;
    }

    public void setSignature(long signature) {
        this.signature = signature;
    }
}
