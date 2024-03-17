// See https://aka.ms/new-console-template for more information
using AES256_DotnetExample;

var key = InfoSec.GenerateKey();

var textEncrypt = "Hello World!!!";
var keyIV = string.Empty;
var textEncrypted = InfoSec.Encrypt(textEncrypt, key, out keyIV);

Console.WriteLine("Text encypted: {0}", textEncrypted);
Console.WriteLine("KeyIV: {0}", keyIV);
Console.WriteLine("Decryt text: {0}", InfoSec.Decrypt(textEncrypted, key, keyIV));