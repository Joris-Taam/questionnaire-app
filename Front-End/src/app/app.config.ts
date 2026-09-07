import { ApplicationConfig, provideZoneChangeDetection, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAuth0 } from '@auth0/auth0-angular';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideServiceWorker } from '@angular/service-worker';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAuth0({
  domain: 'login-tst.snelstart.nl',
  clientId: 'if2pT7yblQtU82yqHebBV2jyigg2GIew',
  useRefreshTokens: true,
  useRefreshTokensFallback: true,
  authorizationParams: {
    audience: 'https://webapi-tst.snelstart.nl',
    redirect_uri: `${window.location.origin}/`
  }
})
, provideClientHydration(withEventReplay()), provideHttpClient(), provideServiceWorker('ngsw-worker.js', {
            enabled: !isDevMode(),
            registrationStrategy: 'registerWhenStable:30000'
          })
  ]
};
