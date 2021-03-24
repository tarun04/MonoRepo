import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromParents from './+state/parents.reducer';
import { ParentsEffects } from './+state/parents.effects';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromParents.PARENTS_FEATURE_KEY,
      fromParents.reducer
    ),
    EffectsModule.forFeature([ParentsEffects]),
  ],
})
export class ParentsStateModule {}
