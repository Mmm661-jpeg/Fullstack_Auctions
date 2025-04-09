export  const formatDates = (date) => //Format dates Used in my and regular bidsCompenents and display AuctionsComponents.
{
    let dateversion = new Date(date);

    let formatedDate = dateversion.toLocaleString("en-GB",
        {
            weekday: "short",  // "Monday"
            year: "numeric",  // "2024"
            month: "short",    // "March"
            day: "numeric",   // "22"
            hour: "2-digit",  // 00:00
            minute: "2-digit", //00:00
            hour12: false      //24 hour time
        }
    )

    return formatedDate
}