import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AuthModule } from './pages/auth/auth.module';
import { HomeModule } from './pages/home/home.module';
import { ListaPedidosModule } from './pages/lista-pedidos/lista-pedidos.module';
import { SharedModule } from './shared/shared.module';
import { TemplateModule } from './template/template.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    TemplateModule,
    AuthModule,
    HomeModule,
    ListaPedidosModule
  ],
  exports: [
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
