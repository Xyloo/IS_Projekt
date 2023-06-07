import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LoadingAnimationComponent } from './loading-animation/loading-animation.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainGraphComponent } from './main-graph/main-graph.component';
import { AuthGuard } from './auth-guard.service';
import { DataIEComponent } from './data-ie/data-ie.component';
import { TokenInterceptor } from './interceptors/token-interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    LoadingAnimationComponent,
    MainGraphComponent,
    MainPageComponent,
    DataIEComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'dataie', component: DataIEComponent , canActivate: [AuthGuard] },
      { path: 'users', component: DataIEComponent, canActivate: [AuthGuard] },
      { path: 'home', component: MainGraphComponent },// canActivate:[AuthGuard] }
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
