package ru.kontur.crypto.money;

import sun.reflect.generics.reflectiveObjects.NotImplementedException;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Scanner;
import java.util.UUID;

public class Program {
    public static void main(String[] args) {
        Cheque cheque = new Cheque(UUID.randomUUID().toString(), 1000);
        Envelope envelope = new Envelope(cheque);

        Bank bank = new Bank();
        envelope.setSignatureFrom(bank);

        bank.checkChequeSignature(cheque);
        int x = 0;
        try{
            FileInputStream fis = new FileInputStream(args[0]);
            try{
                Scanner scanner = new Scanner(fis);

                int n  = scanner.nextInt();
                int sumPerCheque = scanner.nextInt();
                String firstName = scanner.nextLine();
                String secondName = scanner.nextLine();
                String passportSeries = scanner.nextLine();
                String passportNumber = scanner.nextLine();


            }finally {
                try {
                    fis.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }catch (FileNotFoundException ex){

        }
    }

    private static Envelope[] generateEnvelopes(int n, int sumPerCheque){
        throw new NotImplementedException();
    }
}
