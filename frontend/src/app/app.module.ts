import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AuthModule } from './pages/auth/auth.module';
import { HomeModule } from './pages/home/home.module';
import { SharedModule } from './shared/shared.module';
import { TemplateModule } from './template/template.module';
import { NovoPedidoModule } from './pages/novo-pedido/novo-pedido.module';
import { ListaPedidosModule } from './pages/lista-pedidos/lista-pedidos.module';

import { AuthService } from './pages/auth/auth.service';
import { HomeService } from './pages/home/home.service';
import { PedidoService } from './pages/novo-pedido/pedido.service';
import { StdSnackbarService } from './shared/ui-elements/std-snackbar/std-snackbar.service';
import { FinalizarModule } from './pages/finalizar/finalizar.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FlexLayoutModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    SharedModule,
    TemplateModule,
    AuthModule,
    HomeModule,
    ListaPedidosModule,
    NovoPedidoModule,
    FinalizarModule
  ],
  exports: [],
  providers: [
    AuthService,
    HomeService,
    PedidoService,
    StdSnackbarService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
