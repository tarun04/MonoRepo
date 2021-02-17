export interface NavRoutes {
  subheading?: string;
  subroutes: SubRoutes[];
}

export interface SubRoutes {
  route: string;
  title: string;
  icon: string;
  isActive: boolean;
}
