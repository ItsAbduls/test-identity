import { UserManagerSettings } from 'oidc-client';

/* eslint-disable @typescript-eslint/naming-convention */
export class AuthSettings {
    static getClientSettings(): UserManagerSettings {
        return {
            authority: "https://localhost:5000", //appConfig.identityUrl,
            client_id: 'insig_spa',
            redirect_uri: `http://localhost:4200/auth-callback`,
            response_type: 'code',
            scope: 'openid profile email insigapi.read',
            automaticSilentRenew: true,
            silent_redirect_uri: `http://localhost:4200/assets/silent-refresh.html`
        };
    }
}
