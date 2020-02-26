using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSANETFrameworkDemo
{
    class Program
    {
        const string PUBLICKEY = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDhSzPPnFn41iaz+t4tI4kbaXNu
NFOsI8hFeCYtlwPFKRbETHbBS10bMvUbOWLFtRgZV3L924GQ9orbomEmJ1nWyaSO
8iBbZAyiWUP5PJJh/b9kHj1MMwG712bGfYYPdjkRprNpzU9w4UBzUMKKUoHU4c/G
bb4XeBK9LNTPWQL4YwIDAQAB
-----END PUBLIC KEY-----";
        const string PRIVATEKEY = @"-----BEGIN PRIVATE KEY-----
MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAOFLM8+cWfjWJrP6
3i0jiRtpc240U6wjyEV4Ji2XA8UpFsRMdsFLXRsy9Rs5YsW1GBlXcv3bgZD2itui
YSYnWdbJpI7yIFtkDKJZQ/k8kmH9v2QePUwzAbvXZsZ9hg92ORGms2nNT3DhQHNQ
wopSgdThz8Ztvhd4Er0s1M9ZAvhjAgMBAAECgYEAxwNLTUXsJGfn4Gzm/jC52MEZ
+mu2zgT90IAGGZeg+PUG63gwHyeXo4MsCVRz7/m8xAX/ykew+IEQwFt8Pdvc+rrs
5yml4gOBPfhpau5QaI75xNjnyH7UA3mbRCZeyZrvuKqtY/f8pCgzy3EBWnRpkcsq
eE6bsOQrD45mltr+0QECQQDynvhKEh+hD5xBpF/DIP8Fp6fizexHdA6+aZT/gLaF
A4XgZ9HEDDBhvNdadyYUNOLWhkxRHv6CkT5azfLXsJEhAkEA7begtbBCDXDf1+DR
h3j2S8zcv6+utYgcpjvxZqjbPi6UIWXLxI80PIwQ0uouHCUMjikBA6VX9vTbw9TZ
/IelAwJBAKI3W7baiz86mrTg3A4w/5GeWQexuurDVCBHo5F5U493nYk+oOe9ZpPS
mQIpa9JS0d+xB1GtsWlHBzPbQySnL0ECQA/btCjqvT1QTl6EbPXwp92eqQtQmQMb
NW4RiaUjlpyrVs5zkAho1T9EyMqJPNI71n6VVa/8k8WxyAdkZ7ZlBikCQEkNe1+s
AKnh+AFGCJ+6WAq1J2RuIgcA6bVL3ip7F2NHdE+N+tR9JqWw3JNCweWmAlzKIGs6
eKSVD5egzKaLXss=
-----END PRIVATE KEY-----";

        private const string PubKeyXML = "<RSAKeyValue><Modulus>yuW8mDcb1+n/fIKqNaT3LQ3qsKNBg4GC7ZD2KXEJqMOyk5x8JOgwgg3mwnie1LfqryzYHSIJLjxR35WznjrCBT+p07IkitGCPY6JuNI/w1KmaoPueb8V/j8YvPQEs6UIXgj/PJdsw1xPgzIxZj9fyxnXOTqbIee4bTOkT28610yKjiq/90dGvWFRmFWPhjTlet02Dt4Qe0nrK/DMCw2dIIcBqrAJyQCMa8dKObbx0Q7+32X71MB3IyzCWZWou8xMBNAxbIYF3Yu6zjLmcBjWpLAAud3tHp72XJ27sNSfZNR1x4Liqo9NnjOivuRnxIxwCpexBh42Qsfx7JSm3aKeZQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const string PrivKeyXml = "<RSAKeyValue><Modulus>yuW8mDcb1+n/fIKqNaT3LQ3qsKNBg4GC7ZD2KXEJqMOyk5x8JOgwgg3mwnie1LfqryzYHSIJLjxR35WznjrCBT+p07IkitGCPY6JuNI/w1KmaoPueb8V/j8YvPQEs6UIXgj/PJdsw1xPgzIxZj9fyxnXOTqbIee4bTOkT28610yKjiq/90dGvWFRmFWPhjTlet02Dt4Qe0nrK/DMCw2dIIcBqrAJyQCMa8dKObbx0Q7+32X71MB3IyzCWZWou8xMBNAxbIYF3Yu6zjLmcBjWpLAAud3tHp72XJ27sNSfZNR1x4Liqo9NnjOivuRnxIxwCpexBh42Qsfx7JSm3aKeZQ==</Modulus><Exponent>AQAB</Exponent><P>5LPiWxYbwHe/i/IRKJ84B5PrSPdtPqn1Uj/FUu+4ZMpNw3UEyt0u73MLgXLjwCrx7A484cLS4el5z0eOBAnjw2d1Hm6E/jh8sH5zQHv7u1rFefUMYRMYXSqirTVlj226ccFRE9OZ3lKPuXrodappqNstjlprV1AMDxfLHA1aUXs=</P><Q>4x1cWbomvGv8JKlginM9GN0sTcM2BeO961dE2K/zBN5CnmW3um7PvDHyb+ntYYoOaW1lx8V/TB3X5w6ywsMiZhe5uXmqiSWaj8vGAyJ+NM+K2AgHDcEqLFUiyTJ/XkqV2k7iMcSLuRO738OECeC/bEu3HGoGGryJWuEC+fEvGZ8=</Q><DP>B0Bk5vp2es3RNwC/5ofV4PehuDiQMDJ3Yto+yXhsYlW/zXjCZCRLPrBpJvubmRZDgXaaG5Zv1VXv1NCyAhLGNAXtwr9CXEUyPu5jfSHxQ2mHZWyNre5LEXkum0tcIwYZqU214mkNMe1wPTNWd5SlsQLyGNdpG+Wf3EKm4AbUXE0=</DP><DQ>Ea8klLwA7iT+YiBqKv2kIT5/h6KOn1DHZf7KlpDEvHlN+KV08+hS9pVxCjPNzw1/58ej6DVBnzynpg8n7jBhik+In5+QntM1wMKeLXpPF2+doQqm+fQzg3Yxmjb7Ye0u0+vWgweJ1aRquZawvlAot5cBsA21YfmSPGhO4gVcpIM=</DQ><InverseQ>bSssYm1rAcXrZ3G9gDki8Qj0HUUXRrjNJCK2QTHhU5cGY5yiyQE+JVkHOkteKDEaGhaMbGj9cn4D6x22FVV6F9zx+L1RFfrtJpdD9/iYKll6nD3HzpSQ+3AoSE8R8e6bQEWlioW8dICm4A1Acaj7kJyJw1JpJKfjnRrsr05dnQI=</InverseQ><D>KB+GTBOZzfjYLScpwbH9r0sxPf0K15ak7ZXdGBTidB0/EzG+2w2Piih1mb+AqVA1eK7Fjf1NE3eaOTzBaGj2NVOBoft4fnsv5jxpv8LUGSwe/LFaV3kSQFT572PSCjR4kx/0WWcYewmmL6udWTrvFprllMuiIfJQ5kdwFsVIPYrrN7D2A6FOyVuXmmr1DW6+6E/MUjvOWA2UBf4VeybQRsjekaD2ckIM0UK/7+8CWIoNtUzK2ZJ1oqyOk5oVk8Ja0VI3AZSZL1s5Xx/estVZfpmtVGg20T21yXZ7PREpcZQxK67ywNFm12dreZw8sByVvLGKazJfGtijSfHadEPngQ==</D></RSAKeyValue>";


        static void Main(string[] args)
        {
            var publicKey = string.Empty;
            var privateKey = string.Empty;
            RSACryption rsaCryption = new RSACryption();
            
            var encryptString = Console.ReadLine().ToUpper();

            var encryptResult = rsaCryption.RSAEncrypt(PubKeyXML, encryptString);
            Console.WriteLine($"Encrypt Result :{encryptResult}");

            var decryptResult = rsaCryption.RSADecrypt(PrivKeyXml, encryptResult);
            Console.WriteLine($"Decrypt Result :{decryptResult}");


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
