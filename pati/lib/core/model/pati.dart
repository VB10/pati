class Pati {
  double latitude;
  double longitude;
  int percent;

  Pati({this.latitude, this.longitude, this.percent});

  Pati.fromJson(Map<String, dynamic> json) {
    latitude = json['latitude'];
    longitude = json['longitude'];
    percent = json['percent'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['latitude'] = this.latitude;
    data['longitude'] = this.longitude;
    data['percent'] = this.percent;
    return data;
  }
}
