import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';




import { provideNgxMask, NgxMaskConfig } from 'ngx-mask';

import { provideHttpClient } from '@angular/common/http';

const maskConfig: Partial<NgxMaskConfig> = {
  thousandSeparator: '.', // Correct thousands separator
  decimalMarker: ',', // Brazilian decimal marker
  prefix: 'R$', // Adds the R$ prefix
  suffix: '', // No suffix
  dropSpecialCharacters: true, // Keeps only numeric value in FormControl
  showMaskTyped: false, // Displays mask as placeholder
  allowNegativeNumbers: false // Prevent negative values (if needed)
};


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    provideNgxMask(() => maskConfig)
  ]
};
