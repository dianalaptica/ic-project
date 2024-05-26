type Trip = {
    id: number,
    guideId: number,
    adress: string,
    cityName: string,
    description: string,
    title: string,
    image: Blob,
    maxTourist: number,
    startDate: Date,
    endDate: Date
}

export type Trips = {
    hasNextPage: boolean,
    hasPreviousPage: boolean,
    page: number,
    pageSize: number,
    totalCount: number,
    trips: Trip[]
}
