import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { environment } from '../environments/environment';
import { routes } from './app.routes';

// Components
import { FooterComponent, HeaderComponent} from '@app/components';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

// Services
import { AppService, AuthService, DataService, GlobalErrorHandler, ModalService, AuthInterceptor, TimingInterceptor } from '@app/services';

@NgModule({
  declarations: [
    // Components
    AppComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserTransferStateModule,
    HttpClientModule,
    NgbModule.forRoot(),
    RouterModule.forRoot(routes, { initialNavigation: 'enabled' }),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
  ],
  providers: [
    AppService,
    AuthService,
    DataService,
    ModalService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TimingInterceptor, multi: true },
    { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
