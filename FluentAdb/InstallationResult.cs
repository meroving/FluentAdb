namespace FluentAdb
{
    public class InstallationResult
    {
        public static readonly string Success = "SUCCESS";
        public static readonly string InstallFailedAlreadyExists = "INSTALL_FAILED_ALREADY_EXISTS";
        public static readonly string InstallFailedInvalidApk = "INSTALL_FAILED_INVALID_APK";
        public static readonly string InstallFailedInvalidUri = "INSTALL_FAILED_INVALID_URI";
        public static readonly string InstallFailedInsufficientStorage = "INSTALL_FAILED_INSUFFICIENT_STORAGE";
        public static readonly string InstallFailedDuplicatePackage = "INSTALL_FAILED_DUPLICATE_PACKAGE";
        public static readonly string InstallFailedNoSharedUser = "INSTALL_FAILED_NO_SHARED_USER";
        public static readonly string InstallFailedUpdateIncompatible = "INSTALL_FAILED_UPDATE_INCOMPATIBLE";
        public static readonly string InstallFailedSharedUserIncompatible = "INSTALL_FAILED_SHARED_USER_INCOMPATIBLE";
        public static readonly string InstallFailedMissingSharedLibrary = "INSTALL_FAILED_MISSING_SHARED_LIBRARY";
        public static readonly string InstallFailedReplaceCouldntDelete = "INSTALL_FAILED_REPLACE_COULDNT_DELETE";
        public static readonly string InstallFailedDexopt = "INSTALL_FAILED_DEXOPT";
        public static readonly string InstallFailedOlderSdk = "INSTALL_FAILED_OLDER_SDK";
        public static readonly string InstallFailedConflictingProvider = "INSTALL_FAILED_CONFLICTING_PROVIDER";
        public static readonly string InstallFailedNewerSdk = "INSTALL_FAILED_NEWER_SDK";
        public static readonly string InstallFailedTestOnly = "INSTALL_FAILED_TEST_ONLY";
        public static readonly string InstallFailedCpuAbiIncompatible = "INSTALL_FAILED_CPU_ABI_INCOMPATIBLE";
        public static readonly string InstallFailedMissingFeature = "INSTALL_FAILED_MISSING_FEATURE";
        public static readonly string InstallFailedContainerError = "INSTALL_FAILED_CONTAINER_ERROR";
        public static readonly string InstallFailedInvalidInstallLocation = "INSTALL_FAILED_INVALID_INSTALL_LOCATION";
        public static readonly string InstallFailedMediaUnavailable = "INSTALL_FAILED_MEDIA_UNAVAILABLE";
        public static readonly string InstallFailedVerificationTimeout = "INSTALL_FAILED_VERIFICATION_TIMEOUT";
        public static readonly string InstallFailedVerificationFailure = "INSTALL_FAILED_VERIFICATION_FAILURE";
        public static readonly string InstallFailedPackageChanged = "INSTALL_FAILED_PACKAGE_CHANGED";
        public static readonly string InstallFailedUidChanged = "INSTALL_FAILED_UID_CHANGED";
        public static readonly string InstallFailedVersionDowngrade = "INSTALL_FAILED_VERSION_DOWNGRADE";
        public static readonly string InstallFailedPermissionModelDowngrade = "INSTALL_FAILED_PERMISSION_MODEL_DOWNGRADE";
        public static readonly string InstallParseFailedNotApk = "INSTALL_PARSE_FAILED_NOT_APK";
        public static readonly string InstallParseFailedBadManifest = "INSTALL_PARSE_FAILED_BAD_MANIFEST";
        public static readonly string InstallParseFailedUnexpectedException = "INSTALL_PARSE_FAILED_UNEXPECTED_EXCEPTION";
        public static readonly string InstallParseFailedNoCertificates = "INSTALL_PARSE_FAILED_NO_CERTIFICATES";
        public static readonly string InstallParseFailedInconsistentCertificates = "INSTALL_PARSE_FAILED_INCONSISTENT_CERTIFICATES";
        public static readonly string InstallParseFailedCertificateEncoding = "INSTALL_PARSE_FAILED_CERTIFICATE_ENCODING";
        public static readonly string InstallParseFailedBadPackageName = "INSTALL_PARSE_FAILED_BAD_PACKAGE_NAME";
        public static readonly string InstallParseFailedBadSharedUserId = "INSTALL_PARSE_FAILED_BAD_SHARED_USER_ID";
        public static readonly string InstallParseFailedManifestMalformed = "INSTALL_PARSE_FAILED_MANIFEST_MALFORMED";
        public static readonly string InstallParseFailedManifestEmpty = "INSTALL_PARSE_FAILED_MANIFEST_EMPTY";
        public static readonly string InstallFailedInternalError = "INSTALL_FAILED_INTERNAL_ERROR";
        public static readonly string InstallFailedUserRestricted = "INSTALL_FAILED_USER_RESTRICTED";
        public static readonly string InstallFailedDuplicatePermission = "INSTALL_FAILED_DUPLICATE_PERMISSION";
        public static readonly string InstallFailedNoMatchingAbis = "INSTALL_FAILED_NO_MATCHING_ABIS";

        public static readonly string InstallFailedNoSignature = "INSTALL_FAILED_NO_SIGNATURE"; // 0x800B0100
        public static readonly string InstallFailedInvalidPackage = "INSTALL_FAILED_INVALID_PACKAGE"; // 0x80073CF0, 0x80080205
        public static readonly string InstallFailedUntrusted = "INSTALL_FAILED_UNTRUSTED"; // 0x800B0004, 0x800B0109, 0x800B010A
        public static readonly string InstallFailedAccessDenied = "INSTALL_ACCESS_DENIED"; // 0x80070005
        
        public static readonly string InstallCanceled = "INSTALL_CANCELED";
        public static readonly string InstallFailed = "INSTALL_FAILED";
        public static readonly string NotInstalled = "NOT_INSTALL";
    }
}
