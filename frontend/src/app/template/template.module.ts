import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';

import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LayoutComponent } from './layout/layout.component';
import { CozinhaLayoutComponent } from './cozinha-layout/cozinha-layout.component';
import { CozinhaHeaderComponent } from './cozinha-layout/cozinha-header/cozinha-header.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatMenuModule,
    MatButtonModule
  ],
  declarations: [
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    CozinhaLayoutComponent,
    CozinhaHeaderComponent
  ],
  exports: [
    LayoutComponent
  ]
})
export class TemplateModule { }
