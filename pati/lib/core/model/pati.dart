class Pati {
  double latitute;
  int longtitute;
  double weight;

  Pati({this.latitute, this.longtitute, this.weight});

  Pati.fromJson(Map<String, dynamic> json) {
    latitute = json['latitute'];
    longtitute = json['longtitute'];
    weight = json['weight'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['latitute'] = this.latitute;
    data['longtitute'] = this.longtitute;
    data['weight'] = this.weight;
    return data;
  }
}
