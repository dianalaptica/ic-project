type Trip = {
    id: number,
    guideId: number,
    adress: string,
    cityName: string,
    description: string,
    title: string,
    image: Blob,
    maxTourists: number,
    startDate: Date,
    endDate: Date,
    countryName: string,
}

export type Trips = {
    hasNextPage: boolean,
    hasPreviousPage: boolean,
    page: number,
    pageSize: number,
    totalCount: number,
    trips: Trip[]
}
