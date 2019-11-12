class Pati {
  double latitude;
  double longitude;
  double weight;
  double percent;

  Pati({this.latitude, this.longitude, this.weight, this.percent});

  Pati.fromJson(Map<String, dynamic> json) {
    latitude = json['latitude'];
    longitude = json['longitude'];
    weight = json['weight'];
    percent = json['percent'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['latitude'] = this.latitude;
    data['longitude'] = this.longitude;
    data['weight'] = this.weight;
    data['percent'] = this.percent;
    return data;
  }
}