import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromStudents from './+state/students.reducer';
import { StudentsEffects } from './+state/students.effects';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromStudents.STUDENTS_FEATURE_KEY,
      fromStudents.reducer
    ),
    EffectsModule.forFeature([StudentsEffects]),
  ],
})
export class StudentsStateModule {}
