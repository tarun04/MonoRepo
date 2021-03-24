import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromInstructors from './+state/instructors.reducer';
import { InstructorsEffects } from './+state/instructors.effects';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromInstructors.INSTRUCTORS_FEATURE_KEY,
      fromInstructors.reducer
    ),
    EffectsModule.forFeature([InstructorsEffects]),
  ],
})
export class InstructorsStateModule {}
