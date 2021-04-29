export interface AuthUser {
  /**
   * the User's ID.
   */
  sub: string;
  /**
   * The tenant ID this user belongs too.
   */
  tenant_id: string;
  /**
   * The tenant name the user belongs too.
   */
  tenant_name: string;
  email: string;
  email_verified: string;
  given_name: string;
  preferred_username: string;
  /**
   * idToken to be sent as the Authorization header 'Bearer {{idToken}}'.
   */
  idToken: string | null;
}
