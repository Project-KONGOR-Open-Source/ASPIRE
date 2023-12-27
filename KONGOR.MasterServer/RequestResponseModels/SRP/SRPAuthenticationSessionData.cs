﻿namespace KONGOR.MasterServer.RequestResponseModels.SRP;

/// <summary>
///     Exposes the constants, properties, and methods required for Secure Remote Password protocol authentication.
/// </summary>
internal class SRPAuthenticationSessionData
{
    # region Secure Remote Password Protocol Secrets

    // Thank you, Anton Romanov (aka Theli), for making these values public: https://github.com/theli-ua/pyHoNBot/blob/master/hon/masterserver.py#L23-L24.
    // The safe prime number is also present in the k2_x64 DLL, between offsets 0xF2F8A0 and 0xF2FA9F.
    // The multiplicative group generator was expected to be a small number in order to not generate sets unnecessarily, so it could have easily been brute-forced.
    // Having these values in the public domain, however, certainly helped to save a lot of time while writing the closed-source version of Project KONGOR.

    /// <summary>
    ///     N : a safe prime number which must be large enough so that computing discrete logarithms modulo N is infeasible
    /// </summary>
    internal const string SafePrimeNumber = "DA950C6C97918CAE89E4F5ECB32461032A217D740064BC12FC0723CD204BD02A7AE29B53F3310C13BA998B7910F8B6A14112CBC67BDD2427EDF494CB8BCA68510C0AAEE5346BD320845981546873069B337C073B9A9369D500873D647D261CCED571826E54C6089E7D5085DC2AF01FD861AE44C8E64BCA3EA4DCE942C5F5B89E5496C2741A9E7E9F509C261D104D11DD4494577038B33016E28D118AE4FD2E85D9C3557A2346FAECED3EDBE0F4D694411686BA6E65FEE43A772DC84D394ADAE5A14AF33817351D29DE074740AA263187AB18E3A25665EACAA8267C16CDE064B1D5AF0588893C89C1556D6AEF644A3BA6BA3F7DEC2F3D6FDC30AE43FBD6D144BB";

    /// <summary>
    ///     g : a generator of the multiplicative group of integers modulo n
    /// </summary>
    internal const string MultiplicativeGroupGenerator = "2";

    # endregion

    /// <summary>
    ///     The expected length of the salt (and also the length of "B", the ephemeral key of the server).
    /// </summary>
    internal const int SaltLength = 256;

    /// <summary>
    ///     A : the internal ephemeral key sent by the client
    /// </summary>
    internal string ClientInternalEphemeral { get; set; } = null!;

    /// <summary>
    ///     B : the internal ephemeral key generated by the server
    /// </summary>
    internal string ServerInternalEphemeral { get; set; } = null!;

    /// <summary>
    ///     b : the secret ephemeral key generated by the server
    /// </summary>
    internal string ServerPrivateEphemeral { get; set; } = null!;

    /// <summary>
    ///     s : retrieved from the database for I (identifying username); the value is generated during registration and sent by the client
    /// </summary>
    internal string Salt { get; set; } = null!;

    /// <summary>
    ///     The HoN "salt2", retrieved from the database for I (identifying username). The value is generated during registration and sent by the client.
    /// </summary>
    internal string PasswordSalt { get; set; } = null!;

    /// <summary>
    ///     I : the identifying username sent by the client
    /// </summary>
    internal string LoginIdentifier { get; set; } = null!;

    /// <summary>
    ///     The hashed password retrieved from the database for I (identifying username). The value is generated during registration.
    /// </summary>
    internal string PasswordHash { get; set; } = null!;

    /// <summary>
    ///     M1 : the client's proof; the server should verify this value and use it to compute M2 (the server's proof)
    /// </summary>
    internal string ClientProof { get; set; } = null!;

    /// <summary>
    ///     Returns the verifier derived from the client's private key.
    /// </summary>
    internal string Verifier => ComputeVerifier();

    private string ComputeVerifier()
    {
        SrpClient client = new(SrpParameters.Create<SHA256>(SafePrimeNumber, MultiplicativeGroupGenerator));

        string privateClientKey = client.DerivePrivateKey(Salt, LoginIdentifier, PasswordHash);

        return client.DeriveVerifier(privateClientKey);
    }
}