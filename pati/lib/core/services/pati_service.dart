import 'dart:convert';

import 'package:http/http.dart' as http;

class PatiService {
  static PatiService _instance = PatiService._init();
  PatiService._init();
  static PatiService instance = _instance;

  String _baseUrl = "http://192.168.0.103:5102";

  Future<dynamic> getPatiService() async {
    final response = await http.get("$_baseUrl/pet");  
    return json.decode(response.body);
  }
}
