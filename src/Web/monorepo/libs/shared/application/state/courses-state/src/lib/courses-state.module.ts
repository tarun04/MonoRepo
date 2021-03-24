import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromCourses from './+state/courses.reducer';
import { CoursesEffects } from './+state/courses.effects';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromCourses.COURSES_FEATURE_KEY,
      fromCourses.reducer
    ),
    EffectsModule.forFeature([CoursesEffects]),
  ],
})
export class CoursesStateModule {}
