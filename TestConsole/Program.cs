// See https://aka.ms/new-console-template for more information
using JadedEncryption;


Console.WriteLine("Testing One Way Encription");
OnewayEncryption oneWay = new OnewayEncryption();
string toEncript = "this is a demo string";

string encrypedStuff = oneWay.HashData(toEncript);

Console.WriteLine("Data to Encrypt = " + toEncript);
Console.WriteLine("Encryped Data: " + encrypedStuff);

if(oneWay.VerifyHash(encrypedStuff, toEncript)) {
    Console.WriteLine("This one passed");
}

if(!oneWay.VerifyHash(encrypedStuff, "HELLO WORLD"))
{
    Console.WriteLine("This one should have failed");
}


Console.WriteLine("Testing Two Way Encription");

string key = KeyGen.GenerateAesKey(KeySize.KeySize_256);
string iv = KeyGen.GenerateIv();

Console.WriteLine("Key is " + key);
Console.WriteLine("IV is " + iv);

TwoWayEncryption twoWay = new TwoWayEncryption(key, iv);
string twoWayEncryptedString = twoWay.Encrypt("HELLO WORLD");
string twoWayDecryptedString = twoWay.Decrypt(twoWayEncryptedString);

Console.WriteLine("Two way encryptedString : " + twoWayEncryptedString);
Console.WriteLine("Two way decrypted string : " + twoWayDecryptedString);


Console.ReadLine();